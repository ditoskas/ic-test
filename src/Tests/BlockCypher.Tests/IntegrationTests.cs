using BlockCypher.Data;
using BlockCypher.Data.Exceptions;
using BlockCypher.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BlockCypher.Tests
{
    public class IntegrationTests(TestFixture fixture) : IClassFixture<TestFixture>
    {
        private readonly IBlockCypherClient _blockCypherClient = fixture.ServiceProvider.GetRequiredService<IBlockCypherClient>();

        [Fact]
        public async Task GetBlockChainTest()
        {
            BlockCypherChain? result = await _blockCypherClient.GetBlockCypherChain("btc");
            Assert.NotNull(result);
            Assert.Equal(ExpectedResults.BtcMainChain.Name, result!.Name);
            Assert.NotEmpty(result.Hash);
            Assert.NotEmpty(result.PreviousHash);
        }

        [Fact]
        public async Task GetBlockHashTest()
        {
            BlockCypherBlockHash? result = await _blockCypherClient.GetBlockHash("000000000000000000bf56ff4a81e399374a68344a64d6681039412de78366b8", "btc");
            Assert.NotNull(result);
            Assert.Equal(ExpectedResults.BtcBlockHash.Hash, result!.Hash);
            Assert.Equal(ExpectedResults.BtcBlockHash.Chain, result!.Chain);
        }

        [Fact]
        public async Task GetExceptionTest()
        {
            await Assert.ThrowsAsync<BlockCypherException>(async () => await _blockCypherClient.GetBlockCypherChain("invalidChain"));
        }
    }
}
