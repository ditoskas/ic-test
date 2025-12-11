using BlockCypher.Data;
using BlockCypher.Data.Models;

namespace BlockCypher.Client
{
    public class BlockCypherService (IBlockCypherClient blockCypherClient) : IBlockCypherService
    {
        public async Task<List<BlockCypherBlockHash>?> GetLastBlocks(int numberOfBlocks, string coin, string chain = "main", int delay = 0, CancellationToken cancellationToken = default)
        {
            //Get the chain info to find the latest block
            BlockCypherChain? blockCypherChain = await blockCypherClient.GetBlockCypherChain(coin, chain, cancellationToken);
            if (blockCypherChain == null)
            {
                return null;
            }
            List<BlockCypherBlockHash> blockHashes = new();
            string hashToRead = blockCypherChain.Hash;
            for (int i = 0; i < numberOfBlocks; i++)
            {
                BlockCypherBlockHash? blockHash = await blockCypherClient.GetBlockHash(hashToRead, coin, chain, cancellationToken);
                if (blockHash != null)
                {
                    blockHashes.Add(blockHash);
                }
                else
                {
                    break;
                }
                hashToRead = blockHash.PrevBlock;
                if (delay > 0)
                {
                    await Task.Delay(delay, cancellationToken);
                }
            }
            
            return blockHashes;
        }
    }
}
