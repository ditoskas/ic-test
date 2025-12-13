using IcTest.Data.Dtos;
using IcTest.Data.Models;
using IcTest.Infrastructure.Repositories.CryptoRepositories.Contacts;
using IcTest.Shared.ApiResponses;
using IcTest.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IcTest.Infrastructure.Repositories.CryptoRepositories
{
    public class BlockHashRepository(CryptoDbContext cryptoDbContext) : RepositoryBase<BlockHash, CryptoDbContext>(cryptoDbContext), IBlockHashRepository
    {
        public async Task<PaginatedResult<BlockHashDto>> GetHistoryAsync(string chain, int pageNumber, int pageSize,
            bool trackChanges = false, CancellationToken cancellationToken = default)
        {
            IQueryable<BlockHash> query = FindByCondition(bh => bh.Chain == chain, trackChanges)
                                            .OrderByDescending(bh => bh.CreatedAt);

            return await GetPaginateResultListAsync<BlockHashDto>(query, pageNumber, pageSize, cancellationToken);
        }

        public async Task<BlockHash?> GetLastHashAsync(string chain, bool trackChanges = false, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(bh => bh.Chain == chain, trackChanges)
                         .OrderByDescending(bh => bh.CreatedAt)
                         .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
