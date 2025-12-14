using IcTest.Data.Dtos;
using IcTest.Data.Models;
using IcTest.Shared.ApiResponses;
using IcTest.Shared.Repositories.Contacts;

namespace IcTest.Infrastructure.Repositories.CryptoRepositories.Contacts
{
    public interface IBlockHashRepository : IRepositoryBase<BlockHash>
    {
        Task<PaginatedResult<BlockHashDto>> GetHistoryAsync(string chain, int pageNumber, int pageSize,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        Task<BlockHash?> GetLastHashAsync(string chain, bool trackChanges = false,
            CancellationToken cancellationToken = default);
    }
}
