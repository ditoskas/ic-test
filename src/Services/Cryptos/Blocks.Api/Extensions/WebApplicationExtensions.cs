using BlockCypher.Data;
using IcTest.Infrastructure.Database.Seeders;
using IcTest.Infrastructure.Repositories.CryptoRepositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Blocks.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<CryptoDbContext>();
            IBlockCypherService blockCypherService = scope.ServiceProvider.GetRequiredService<IBlockCypherService>();

            await context.Database.MigrateAsync();

            await CryptoDatabaseSeeder.SeedAsync(context, blockCypherService);
        }

        public static void UseSwaggerPipeline(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
                // Add Swagger UI
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "Blocks API v1");
                });
            }
        }
    }
}
