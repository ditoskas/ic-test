using BlockCypher.Client;
using BlockCypher.Client.RateLimit;
using BlockCypher.Data;
using BlockCypher.Data.Models;
using IcTest.Data.Models;
using IcTest.Infrastructure.Repositories.CryptoRepositories.Contacts;
using IcTest.Shared.BackgroundServices;
using IcTest.Shared.Constants;
using IcTest.Shared.Helpers;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.TeamFoundation.Build.WebApi;

namespace IcTest.Infrastructure.BackgroundServices
{
    public class BlocksImporterService (
        ILogger<BlocksImporterService> logger,
        IServiceScopeFactory serviceScopeFactory,
        IBlockCypherClient blockCypherClient,
        IBlockCypherService blockCypherService
        ) : BackgroundService, IBaseBackgroundService
    {
        public string Status => _isRunning ? BackgroundServiceStatus.Running : BackgroundServiceStatus.Stopped;
        private bool _isRunning = false;
        private static readonly TimeSpan PollInterval = TimeSpan.FromMinutes(1);
        private ICryptoRepositoryManager cryptoRepositoryManager;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _isRunning = true;
            try
            {
                using var scope = serviceScopeFactory.CreateScope();
                cryptoRepositoryManager = scope.ServiceProvider.GetRequiredService<ICryptoRepositoryManager>();

                using PeriodicTimer timer = new(PollInterval);
                do
                {
                    List<BlockChain> blockChainsToRead =
                        await cryptoRepositoryManager.BlockChainRepository.GetActiveChainsAsync(false, stoppingToken);
                    if (blockChainsToRead.Count > 0)
                    {
                        logger.LogInformation($"There are [{blockChainsToRead.Count}] active blockchains to read");
                        foreach (BlockChain chain in blockChainsToRead)
                        {
                            logger.LogInformation($"Importing blocks for blockchain: {chain.Name}");
                            await ImportBlocksForChain(chain, stoppingToken);
                        }
                    }
                    else
                    {
                        logger.LogWarning($"There is no active blockchains to read");
                    }
                } while (await timer.WaitForNextTickAsync(stoppingToken));
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                logger.LogWarning($"BlocksImporterService is stopping due to cancellation.");
            }
            catch (Exception exception)
            {
                logger.LogCritical(exception, "An unexpected error occurred in BlocksImporterService.");
            }
            finally
            {
                _isRunning = false;
            }
        }

        private async Task ImportBlocksForChain(BlockChain chainToImport, CancellationToken cancellationToken)
        {
            try
            {
                List<BlockCypherBlockHash> blockHashesFromApi = new();
                //Read last saved block hash
                BlockHash? lastSavedBlockHash =
                    await cryptoRepositoryManager.BlockHashRepository.GetLastHashAsync(chainToImport.Name,
                        false, cancellationToken);
                if (lastSavedBlockHash == null)
                {
                    logger.LogError($"There is not last saved block hash for blockchain [{chainToImport.Name}]");
                    return;
                }

                //Read latest chain info from BlockCypher
                BlockCypherChain? blockCypherChain = await blockCypherService.AcquireAndExecuteAsync(
                    async () => await blockCypherClient.GetBlockCypherChain(
                        chainToImport.Coin,
                        chainToImport.Chain,
                        cancellationToken),
                    "GetBlockCypherChain",
                    chainToImport.Name,
                    cancellationToken);
                
                if (blockCypherChain == null)
                {
                    logger.LogError(
                        $"Blockchain info not found for coin[{chainToImport.Coin}] in chain [{chainToImport.Chain}]");
                    return;
                }

                string hashToStop = lastSavedBlockHash.Hash;
                string hashToRead = blockCypherChain.Hash;
                // Start reading from latest block
                while (!cancellationToken.IsCancellationRequested && hashToRead != hashToStop)
                {
                    //Read block hash info
                    BlockCypherBlockHash? blockHash = await blockCypherService.AcquireAndExecuteAsync(
                        async () => await blockCypherClient.GetBlockHash(
                            hashToRead,
                            chainToImport.Coin,
                            chainToImport.Chain,
                            cancellationToken),
                        "GetBlockHash",
                        chainToImport.Name,
                        cancellationToken);

                    if (blockHash != null)
                    {
                        logger.LogInformation($"Has block read: {blockHash.Hash}");
                        blockHashesFromApi.Add(blockHash);
                    }
                    else
                    {
                        logger.LogWarning($"Has block not found: {hashToRead}");
                        break;
                    }

                    hashToRead = blockHash.PrevBlock;
                }

                //All the blocks read, now save them in reverse order
                blockHashesFromApi.Reverse();
                List<BlockHash> blockHashesToAdd = blockHashesFromApi.Adapt<List<BlockHash>>();
                logger.LogInformation($"BlocksImporterService is saving [{blockHashesToAdd.Count}] blocks.");
                await cryptoRepositoryManager.BlockHashRepository.CreateRangeAsync(blockHashesToAdd);
                await cryptoRepositoryManager.SaveChangesAsync(cancellationToken);
            }
            catch (RateLimitExceededException ex) // if BlockCypher client exposes this
            {
                logger.LogWarning(
                    ex,
                    "BlockCypher rate limit exceeded for blockchain: {BlockChainName}. Backing off.",
                    chainToImport.Name);

                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error importing blocks for blockchain: {BlockChainName}", chainToImport.Name);
                if (ex.Message.Contains("Limits reached"))
                {
                    logger.LogWarning(
                        ex,
                        "BlockCypher rate limit exceeded for blockchain: {BlockChainName}. Backing off.",
                        chainToImport.Name);
                    await Task.Delay(TimeSpan.FromSeconds(60), cancellationToken);
                }
                else
                {
                    throw;
                }
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"BlocksImporterService is starting.");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"BlocksImporterService is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
