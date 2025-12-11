using BlockCypher.Data;
using IcTest.Data.Models;
using IcTest.Infrastructure.Repositories.CryptoRepositories;
using Mapster;

namespace IcTest.Infrastructure.Database.Seeders
{
    public class BlockHashesSeeder
    {
        const int BlockTransactionsToSeed = 3;
        const int DelayBetweenRequestsMs = 2000;

        public static async Task Seed(CryptoDbContext context, IBlockCypherService blockCypherService)
        {
            // we add the latest 3 blocks per blockchain so the background service will have data to start processing
            if (!context.BlockTransactions.Any())
            {

                foreach (BlockChain blockChain in BlockChainSeeder.BlockChainsToAdd)
                {
                    var blockHashes = await blockCypherService.GetLastBlocks(BlockTransactionsToSeed, blockChain.Coin, blockChain.Chain, DelayBetweenRequestsMs);
                    if (blockHashes is not null)
                    {
                        // Reverse to start from the oldest block
                        blockHashes.Reverse();
                        // Map BlockCypherBlockHash to BlockTransaction
                        List<BlockHash> blockHashesToAdd = blockHashes.Adapt<List<BlockHash>>();
                        // Add Records
                        context.BlockHashes.AddRange(blockHashesToAdd);
                    }

                    await Task.Delay(DelayBetweenRequestsMs);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
