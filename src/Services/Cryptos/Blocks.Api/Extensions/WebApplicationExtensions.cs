using IcTest.Infrastructure.Database.Seeders;
using IcTest.Infrastructure.Repositories.CryptoRepositories;
using Microsoft.EntityFrameworkCore;

namespace Blocks.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<CryptoDbContext>();

            await context.Database.MigrateAsync();

            await CryptoDatabaseSeeder.SeedAsync(context);
        }
    }
}
