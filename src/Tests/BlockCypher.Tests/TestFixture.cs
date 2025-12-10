using BlockCypher.Data.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using BlockCypher.Client.Extensions;

namespace BlockCypher.Tests
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
            BlockCypherConfig blockCypherConfig = Config.GetSection("BlockCypher").Get<BlockCypherConfig>()!;

            services.AddBlockCypherHttpClient(blockCypherConfig);
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
