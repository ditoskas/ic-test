using BlockCypher.Data.Models;

namespace BlockCypher.Data
{
    public interface IBlockCypherClient
    {
        Task<BlockCypherChain?> GetBlockCypherChain(string coin, string chain = "main", CancellationToken cancellationToken = default);

        Task<BlockCypherBlockHash?> GetBlockHash(string hash, string coin, string chain = "main", CancellationToken cancellationToken = default);
    }
}
