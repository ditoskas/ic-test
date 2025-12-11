using BlockCypher.Client.Extensions;
using BlockCypher.Data.Config;
using IcTest.Infrastructure.Extensions;
using System.Reflection;

namespace Blocks.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            services.AddOpenApi();

            // Add Swagger services (required for UseSwagger/UseSwaggerUI)
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        public static IServiceCollection AddHandlersAndServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Services
            services.AddCryptoDatabaseServices(configuration);

            //Handlers
            BlockCypherConfig blockCypherConfig = configuration.GetSection("BlockCypher").Get<BlockCypherConfig>()!;
            services.AddBlockCypherServices(blockCypherConfig);
            return services;
        }

        public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
        {
            // Configure CORS with the "AllowAll" policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            return services;
        }

        public static IServiceCollection AddCarterAndMediatR(this IServiceCollection services)
        {
            //Map Carter Modules
            Assembly blockTransactionsModuleAssembly = typeof(BlockTransactionsModule.AssemblyReference).Assembly;
            services.AddCarterWithAssemblies(blockTransactionsModuleAssembly);

            //Add MediatR
            services.AddMediatRWithAssemblies(blockTransactionsModuleAssembly);
            return services;
        }

        public static IServiceCollection AddSystemHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("CryptoDatabase") ?? "");
            return services;
        }
    }
}
