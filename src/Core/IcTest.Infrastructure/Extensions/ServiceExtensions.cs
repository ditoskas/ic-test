using Carter;
using FluentValidation;
using IcTest.Infrastructure.Repositories.CryptoRepositories;
using IcTest.Infrastructure.Repositories.CryptoRepositories.Contacts;
using IcTest.Infrastructure.Repositories.CryptoRepositories.Decorators;
using IcTest.Infrastructure.Services.Cache;
using IcTest.Shared.Behaviors;
using IcTest.Shared.Interceptors;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using StackExchange.Redis;
using System.Reflection;
using IDatabase = StackExchange.Redis.IDatabase;

namespace IcTest.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("CryptoRedis") ?? throw new InvalidOperationException("CryptoRedis connection string is not configured.");
            services.AddSingleton<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect(connectionString));

            services.AddSingleton<IDatabase>(provider =>
            {
                var multiplexer = provider.GetRequiredService<IConnectionMultiplexer>();
                return multiplexer.GetDatabase();
            });
            services.AddSingleton<ICacheService, RedisService>();

            return services;
        }

        /// <summary>
        /// Dependency injection for MediatR with assemblies
        /// example:
        /// var module1 = typeof(Module1Service).Assembly;
        /// var module2 = typeof(Module2Service).Assembly;
        /// var module3 = typeof(Module3Service).Assembly;
        /// builder.Services.AddMediatRWithAssemblies(module1, module2, module3);
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddMediatRWithAssemblies(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(assemblies);
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssemblies(assemblies);
            //Configure route handler to throw exceptions in order to be received by the middleware
            services.Configure<RouteHandlerOptions>(o =>
            {
                o.ThrowOnBadRequest = true;
            });
            return services;
        }

        /// <summary>
        /// Add carter with assemblies
        /// example:
        /// var module1 = typeof(Module1Service).Assembly;
        /// var module2 = typeof(Module2Service).Assembly;
        /// var module3 = typeof(Module3Service).Assembly;
        /// builder.Services.AddCarterWithAssemblies(module1, module2, module3);
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddCarterWithAssemblies(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddCarter(configurator: config =>
            {
                foreach (var assembly in assemblies)
                {
                    var modules = assembly.GetTypes()
                        .Where(t => t.IsAssignableTo(typeof(ICarterModule))).ToArray();

                    config.WithModules(modules);
                }
            });


            return services;
        }

        /// <summary>
        /// Add Crypto Database Services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCryptoDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure Npgsql to enable dynamic JSON serialization
            string connectionString = configuration.GetConnectionString("CryptoDatabase") ?? throw new InvalidOperationException("CryptoDatabase connection string is not configured.");
            // Add services to the container.
            services.AddScoped<ISaveChangesInterceptor, TimestampEntityInterceptor>();
            services.AddScoped<ICryptoDbContext, CryptoDbContext>();
            //Repositories
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
