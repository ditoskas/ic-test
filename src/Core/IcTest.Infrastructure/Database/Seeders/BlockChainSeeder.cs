using IcTest.Data.Models;
using IcTest.Infrastructure.Repositories.CryptoRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcTest.Infrastructure.Database.Seeders
{
    public class BlockChainSeeder
    {
        public static async Task Seed(CryptoDbContext context)
        {
            if (!context.BlockChains.Any())
            {
                context.BlockChains.AddRange(BlockChainsToAdd);
                await context.SaveChangesAsync();
            }
        }

        private static readonly List<BlockChain> BlockChainsToAdd =
        [
            new BlockChain
            {
                Coin = "btc",
                Chain = "main",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new BlockChain
            {
                Coin = "btc",
                Chain = "test3",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new BlockChain
            {
                Coin = "eth",
                Chain = "main",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new BlockChain
            {
                Coin = "dash",
                Chain = "main",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new BlockChain
            {
                Coin = "ltc",
                Chain = "main",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
        ];
    }
}
