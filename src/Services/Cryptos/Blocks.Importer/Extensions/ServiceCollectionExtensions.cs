using BlockCypher.Client.Extensions;
using BlockCypher.Data.Config;
using IcTest.Infrastructure.BackgroundServices;
using IcTest.Infrastructure.Extensions;
using IcTest.Shared.BackgroundServices;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Blocks.Importer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHandlersAndServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Cache
            services.AddRedis(configuration);
            //Database
            services.AddCryptoDatabaseServices(configuration);
            //Handlers
            BlockCypherConfig blockCypherConfig = configuration.GetSection("BlockCypher").Get<BlockCypherConfig>()!;
            services.AddBlockCypherServices(blockCypherConfig);
            //Background Services
            services.AddHostedService<BlocksImporterService>();
            return services;
        }

        public static IServiceCollection AddSystemHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("CryptoDatabase") ?? string.Empty)
                .AddRedis(configuration.GetConnectionString("CryptoRedis") ?? string.Empty)
                .Add(new HealthCheckRegistration(
                    nameof(BlocksImporterService),
                    sp =>
                    {
                        var service = sp.GetRequiredService<IBlocksImporterService>();
                        return new BackgroundServiceHealthCheck(service.Status);
                    },
                    HealthStatus.Unhealthy,
                    null));
            return services;
        }
    }
}
