using IcTest.Data.Models;
using IcTest.Shared.Repositories.Contacts;
using Microsoft.EntityFrameworkCore;

namespace IcTest.Infrastructure.Repositories.CryptoRepositories.Contacts
{
    public interface IBlockChainRepository : IRepositoryBase<BlockChain>
    {
        Task<List<BlockChain>> GetActiveChainsAsync(bool trackChanges = false, CancellationToken cnt = default);
    }
}
