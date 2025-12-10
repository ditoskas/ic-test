using IcTest.Infrastructure.Repositories.CryptoRepositories;

namespace IcTest.Infrastructure.Database.Seeders
{
    public class CryptoDatabaseSeeder
    {
        public static async Task SeedAsync(CryptoDbContext context)
        {
            await BlockChainSeeder.Seed(context);
        }
    }
}
