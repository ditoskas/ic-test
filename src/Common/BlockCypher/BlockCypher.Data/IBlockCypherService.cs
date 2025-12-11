using BlockCypher.Data.Models;

namespace BlockCypher.Data
{
    public interface IBlockCypherService
    {
        Task<List<BlockCypherBlockHash>?> GetLastBlocks(int numberOfBlocks, string coin, string chain = "main", int delay = 0,
            CancellationToken cancellationToken = default);
    }
}
