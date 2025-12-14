using BlockCypher.Client;
using BlockCypher.Data;
using IcTest.Infrastructure.Repositories.CryptoRepositories;

namespace IcTest.Infrastructure.Database.Seeders
{
    public class CryptoDatabaseSeeder
    {
        public static async Task SeedAsync(CryptoDbContext context, IBlockCypherService blockCypherService)
        {
            await BlockChainSeeder.Seed(context);
            await BlockHashesSeeder.Seed(context, blockCypherService);
        }
    }
}
