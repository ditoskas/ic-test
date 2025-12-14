using IcTest.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RepositoryManager.Tests
{
    public class TestFixture
    {
        public IServiceProvider ServiceProvider { get; }
        private static readonly IConfiguration Config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        public TestFixture()
        {
            ServiceCollection services = new ServiceCollection();
            //Cache
            services.AddRedis(Config);
            //Database
            services.AddCryptoDatabaseServices(Config);
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
