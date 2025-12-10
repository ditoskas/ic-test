using BlockCypher.Data.Models;

namespace BlockCypher.Data
{
    public interface IBlockCypherClient
    {
        Task<BlockCypherChain?> GetBlockCypherChain(string chain, string network = "main", CancellationToken cancellationToken = default);

        Task<BlockCypherBlockHash?> GetBlockHash(string hash, string chain, string network = "main", CancellationToken cancellationToken = default);
    }
}
