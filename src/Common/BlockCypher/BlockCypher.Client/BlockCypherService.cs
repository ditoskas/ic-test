using BlockCypher.Client.RateLimit;
using BlockCypher.Data;
using BlockCypher.Data.Models;
using Microsoft.Extensions.Logging;

namespace BlockCypher.Client
{
    public class BlockCypherService (IBlockCypherClient blockCypherClient, ILogger<BlockCypherService> logger) : IBlockCypherService
    {
        public async Task<T?> AcquireAndExecuteAsync<T>(
            Func<Task<T>> action,
            string operationName,
            string blockChainName,
            CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using var lease = await BlockCypherRateLimiter.AcquireAsync(cancellationToken);

                if (lease.IsAcquired)
                {
                    return await action();
                }

                logger.LogWarning(
                    $"Rate limiter did not grant lease for operation {operationName} on blockchain {blockChainName}. Retrying...");

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }

            return default;
        }

        public async Task<List<BlockCypherBlockHash>?> GetLastBlocks(int numberOfBlocks, string coin, string chain = "main", int delay = 0, CancellationToken cancellationToken = default)
        {
            //Get the chain info to find the latest block
            BlockCypherChain? blockCypherChain = await blockCypherClient.GetBlockCypherChain(coin, chain, cancellationToken);
            if (blockCypherChain == null)
            {
                return null;
            }
            List<BlockCypherBlockHash> blockHashes = new();
            string hashToRead = blockCypherChain.Hash;
            for (int i = 0; i < numberOfBlocks; i++)
            {
                BlockCypherBlockHash? blockHash = await blockCypherClient.GetBlockHash(hashToRead, coin, chain, cancellationToken);
                if (blockHash != null)
                {
                    blockHashes.Add(blockHash);
                }
                else
                {
                    break;
                }
                hashToRead = blockHash.PrevBlock;
                if (delay > 0)
                {
                    await Task.Delay(delay, cancellationToken);
                }
            }
            
            return blockHashes;
        }

        public async Task<List<BlockCypherBlockHash>?> GetBlocksUntil(string hashToStop, string coin, string chain = "main", int delay = 0, CancellationToken cancellationToken = default)
        {
            //Get the chain info to find the latest block
            BlockCypherChain? blockCypherChain = await blockCypherClient.GetBlockCypherChain(coin, chain, cancellationToken);
            if (blockCypherChain == null)
            {
                return null;
            }
            List<BlockCypherBlockHash> blockHashes = new();
            string hashToRead = blockCypherChain.Hash;
            while (hashToStop != hashToRead)
            {
                BlockCypherBlockHash? blockHash = await blockCypherClient.GetBlockHash(hashToRead, coin, chain, cancellationToken);
                if (blockHash != null)
                {
                    blockHashes.Add(blockHash);
                }
                else
                {
                    break;
                }
                hashToRead = blockHash.PrevBlock;
                if (delay > 0)
                {
                    await Task.Delay(delay, cancellationToken);
                }
            }

            return blockHashes;
        }
    }
}
