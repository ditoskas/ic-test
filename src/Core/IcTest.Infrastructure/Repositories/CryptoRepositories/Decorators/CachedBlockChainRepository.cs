using IcTest.Data.Models;
using IcTest.Infrastructure.Repositories.CryptoRepositories.Contacts;
using IcTest.Infrastructure.Services.Cache;

namespace IcTest.Infrastructure.Repositories.CryptoRepositories.Decorators
{
    public class CachedBlockChainRepository(IBlockChainRepository innerRepository, ICacheService cacheService) : CachedBaseRepository<BlockChain>(innerRepository, cacheService), IBlockChainRepository
    {
        public async Task<List<BlockChain>> GetActiveChainsAsync(bool trackChanges = false, CancellationToken cnt = default)
        {
            string cacheKey = RedisService.BuildRedisKeyFromParameters(BuildMethodCacheKey(), trackChanges);
            TimeSpan? expiryTime = DefaultCacheTimeInMinutes.HasValue ? TimeSpan.FromMinutes(DefaultCacheTimeInMinutes.Value) : null;
            List<BlockChain>? result = await cacheService.GetOrSetDataAsync(cacheKey, () => innerRepository.GetActiveChainsAsync(trackChanges, cnt), expiryTime);
            return result ?? [];
        }
    }
}
