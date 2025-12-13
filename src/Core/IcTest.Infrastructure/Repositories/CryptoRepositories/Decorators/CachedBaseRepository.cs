using IcTest.Infrastructure.Services.Cache;
using IcTest.Shared.ApiResponses;
using IcTest.Shared.Repositories.Contacts;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace IcTest.Infrastructure.Repositories.CryptoRepositories.Decorators
{
    public class CachedBaseRepository<T>(IRepositoryBase<T> innerRepository, ICacheService cacheService) : IRepositoryBase<T> where T : class
    {
        protected string CacheKeyPrefix = typeof(T).Name + ":";
        protected long? DefaultCacheTimeInMinutes = 5;

        protected string BuildMethodCacheKey([CallerMemberName] string memberName = "")
        {
            return CacheKeyPrefix + memberName;
        }

        #region CachableMethods
        public T? GetById(long id, bool trackChanges)
        {
            string cacheKey = RedisService.BuildRedisKeyFromParameters(BuildMethodCacheKey(), id, trackChanges);
            TimeSpan? expiryTime = DefaultCacheTimeInMinutes.HasValue ? TimeSpan.FromMinutes(DefaultCacheTimeInMinutes.Value) : null;
            return cacheService.GetOrSetData<T>(cacheKey, () => innerRepository.GetById(id, trackChanges), expiryTime);
        }

        public async Task<T?> GetByIdAsync(long id, bool trackChanges, CancellationToken cnt = default)
        {
            string cacheKey = RedisService.BuildRedisKeyFromParameters(BuildMethodCacheKey(), id, trackChanges);
            TimeSpan? expiryTime = DefaultCacheTimeInMinutes.HasValue ? TimeSpan.FromMinutes(DefaultCacheTimeInMinutes.Value) : null;
            return await cacheService.GetOrSetDataAsync<T>(cacheKey, () => innerRepository.GetByIdAsync(id, trackChanges, cnt), expiryTime);
        }

        public async Task<List<T>> GetPagedListAsync(IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cnt = default)
        {
            string cacheKey = RedisService.BuildRedisKeyFromParameters(BuildMethodCacheKey(), pageNumber, pageSize);
            TimeSpan? expiryTime = DefaultCacheTimeInMinutes.HasValue ? TimeSpan.FromMinutes(DefaultCacheTimeInMinutes.Value) : null;
                return await cacheService.GetOrSetDataAsync<List<T>>(cacheKey, () => innerRepository.GetPagedListAsync(query, pageNumber, pageSize, cnt), expiryTime) ?? new List<T>();

        }

        public async Task<PaginatedResult<TDto>> GetPaginateResultListAsync<TDto>(IQueryable<T> query, int pageNumber,
            int pageSize, CancellationToken cnt = default)
        {
            string cacheKey = RedisService.BuildRedisKeyFromParameters(BuildMethodCacheKey(), pageNumber, pageSize);
            TimeSpan? expiryTime = DefaultCacheTimeInMinutes.HasValue ? TimeSpan.FromMinutes(DefaultCacheTimeInMinutes.Value) : null;
            return await cacheService.GetOrSetDataAsync<PaginatedResult<TDto>>(
                cacheKey, 
                () => innerRepository.GetPaginateResultListAsync<TDto>(query, pageNumber, pageSize, cnt),
                expiryTime) ?? new PaginatedResult<TDto>(pageNumber, pageSize, 0, new List<TDto>());
        }
        #endregion

        #region Non Cachable Methods
        public IQueryable<T> FindAll(bool trackChanges)
        {
            return innerRepository.FindAll(trackChanges);
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return innerRepository.FindByCondition(expression, trackChanges);
        }

        public void Create(T entity)
        {
            innerRepository.Create(entity);
        }

        public async Task CreateAsync(T entity)
        {
            await innerRepository.CreateAsync(entity);
        }

        public void CreateRange(IList<T> entities)
        {
            innerRepository.CreateRange(entities);
        }
        public async Task CreateRangeAsync(IList<T> entities)
        {
            await innerRepository.CreateRangeAsync(entities);
        }
        public void Update(T entity)
        {
            innerRepository.Update(entity);
        }
        public void Delete(T entity)
        {
            innerRepository.Delete(entity);
        }
        #endregion
    }
}
