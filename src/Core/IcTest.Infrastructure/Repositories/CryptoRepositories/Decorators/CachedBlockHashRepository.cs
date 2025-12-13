using IcTest.Data.Dtos;
using IcTest.Data.Models;
using IcTest.Infrastructure.Repositories.CryptoRepositories.Contacts;
using IcTest.Infrastructure.Services.Cache;
using IcTest.Shared.ApiResponses;

namespace IcTest.Infrastructure.Repositories.CryptoRepositories.Decorators
{
    public class CachedBlockHashRepository(IBlockHashRepository innerRepository, ICacheService cacheService) : CachedBaseRepository<BlockHash>(innerRepository, cacheService), IBlockHashRepository
    {
        public async Task<PaginatedResult<BlockHashDto>> GetHistoryAsync(string chain, int pageNumber, int pageSize,
            bool trackChanges = false, CancellationToken cancellationToken = default)
        {
            string cacheKey = RedisService.BuildRedisKeyFromParameters(BuildMethodCacheKey(), chain, pageNumber, pageSize, trackChanges);
            TimeSpan? expiryTime = DefaultCacheTimeInMinutes.HasValue ? TimeSpan.FromMinutes(DefaultCacheTimeInMinutes.Value) : null;
            PaginatedResult<BlockHashDto>? result = await cacheService.GetOrSetDataAsync(cacheKey, () => innerRepository.GetHistoryAsync(chain, pageNumber, pageSize, trackChanges), expiryTime);

            if (result is null)
            {
                return new PaginatedResult<BlockHashDto>(0, 0, 0, new List<BlockHashDto>());
            }

            return result;
        }
    }
}
