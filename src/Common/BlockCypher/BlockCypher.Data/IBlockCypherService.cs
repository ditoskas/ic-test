using BlockCypher.Data.Models;

namespace BlockCypher.Data
{
    public interface IBlockCypherService
    {
        Task<List<BlockCypherBlockHash>?> GetLastBlocks(int numberOfBlocks, string coin, string chain = "main", int delay = 0,
            CancellationToken cancellationToken = default);

        Task<List<BlockCypherBlockHash>?> GetBlocksUntil(string hashToStop, string coin, string chain = "main",
            int delay = 0, CancellationToken cancellationToken = default);

        Task<T?> AcquireAndExecuteAsync<T>(Func<Task<T>> action, string operationName, string blockChainName, CancellationToken cancellationToken);
    }
}
