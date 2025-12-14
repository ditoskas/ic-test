using IcTest.Data.Models;
using IcTest.Infrastructure.Repositories.CryptoRepositories.Contacts;
using IcTest.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IcTest.Infrastructure.Repositories.CryptoRepositories
{
    public class BlockChainRepository(CryptoDbContext cryptoDbContext) : RepositoryBase<BlockChain, CryptoDbContext>(cryptoDbContext), IBlockChainRepository
    {
        public async Task<List<BlockChain>> GetActiveChainsAsync(bool trackChanges = false, CancellationToken cnt = default)
        {
            return await FindAll(false).OrderBy(bc => bc.Coin).ToListAsync(cnt);
        }
    }
}
