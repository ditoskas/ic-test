using IcTest.Infrastructure.Repositories.CryptoRepositories;
using IcTest.Infrastructure.Repositories.CryptoRepositories.Contacts;
using IcTest.Shared.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace IcTest.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCryptoDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure Npgsql to enable dynamic JSON serialization
            var connectionString = configuration.GetConnectionString("CryptoDatabase");
            // Add services to the container.
            services.AddScoped<ISaveChangesInterceptor, TimestampEntityInterceptor>();
            services.AddScoped<ICryptoDbContext, CryptoDbContext>();
            services.AddScoped<ICryptoRepositoryManager, CryptoRepositoryManager>();

            // Configure the DbContext with Npgsql and custom settings
            NpgsqlDataSourceBuilder dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            dataSourceBuilder.UseJsonNet();
            var dataSource = dataSourceBuilder.Build();

            services.AddDbContext<CryptoDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(dataSource, npgsqlOptions =>
                {
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5, // Retry up to 5 times
                        maxRetryDelay: TimeSpan.FromSeconds(5), // Maximum delay between retries
                        errorCodesToAdd: null); // Use default transient error codes
                    npgsqlOptions.CommandTimeout(600); // Set timeout to 10 minutes (600 seconds)
                });
            });

            return services;
        }
    }
}
