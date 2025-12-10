using BlockCypher.Data;
using BlockCypher.Data.Config;
using Microsoft.Extensions.DependencyInjection;

namespace BlockCypher.Client.Extensions
{
    public static class BlockCypherServiceCollectionExtensions
    {
        public static void AddBlockCypherHttpClient(this IServiceCollection services, BlockCypherConfig config)
        {
            services.AddHttpClient<IBlockCypherClient, BlockCypherClient>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(config.BaseUrl);
            });
        }
    }
}
