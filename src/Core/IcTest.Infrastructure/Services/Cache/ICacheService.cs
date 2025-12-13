namespace IcTest.Infrastructure.Services.Cache
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key);
        T? Get<T>(string key);
        Task<bool> SetAsync<T>(string key, T data, TimeSpan? absoluteExpireTime = null);
        bool Set<T>(string key, T data, TimeSpan? absoluteExpireTime = null);
        Task<bool> RemoveAsync(string key);
        bool ContainsKey(string key);
        Task<T?> GetOrSetDataAsync<T>(string key, Func<Task<T>> factory, TimeSpan? absoluteExpireTime = null);
        T? GetOrSetData<T>(string key, Func<T> factory, TimeSpan? absoluteExpireTime = null);
    }
}
