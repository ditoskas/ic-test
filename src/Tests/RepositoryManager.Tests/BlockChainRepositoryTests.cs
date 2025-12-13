using IcTest.Data.Models;
using IcTest.Infrastructure.Repositories.CryptoRepositories.Contacts;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace RepositoryManager.Tests
{
    public class BlockChainRepositoryTests(TestFixture fixture) : IClassFixture<TestFixture>
    {
        readonly ICryptoRepositoryManager _repositoryManager = fixture.ServiceProvider.GetRequiredService<ICryptoRepositoryManager>();
        private static readonly string[] Expected = ["ada", "btc", "eth"];

        [Fact]
        public async Task GetActiveChainsAsyncReturnsChainsOrderedByCoin()
        {
            List <BlockChain> blockChains =
            [
                new BlockChain { Coin = "eth", Chain = "main" },
                new BlockChain { Coin = "btc", Chain = "main" },
                new BlockChain { Coin = "ada", Chain = "main" }
            ];

            await _repositoryManager.BlockChainRepository.CreateRangeAsync(blockChains);
            await _repositoryManager.SaveChangesAsync();

            List<BlockChain> result = await _repositoryManager.BlockChainRepository.GetActiveChainsAsync();

            Assert.Equal(3, result.Count);
            Assert.Equal(Expected, result.Select(bc => bc.Coin));
        }
    }
}
