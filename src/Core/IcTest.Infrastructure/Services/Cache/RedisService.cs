using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;

namespace IcTest.Infrastructure.Services.Cache
{
    /// <summary>
    /// Redis wrapper service
    /// </summary>
    /// <param name="redisDatabase"></param>
    /// <param name="logger"></param>
    public class RedisService (IDatabase redisDatabase, ILogger<RedisService> logger) : ICacheService
    {
        public static string BuildRedisKeyFromParameters(string prefix, params object?[] parameters)
        {
            string key = prefix;
            foreach (var param in parameters)
            {
                key += $":{param}";
            }
            return key;
        }

        public T? Get<T>(string key)
        {
            var value = redisDatabase.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }

            return default;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await redisDatabase.StringGetAsync(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }

            return default;
        }

        public async Task<bool> SetAsync<T>(string key, T data, TimeSpan? absoluteExpireTime = null)
        {
            var serializedData = JsonSerializer.Serialize(data);
            if (absoluteExpireTime.HasValue)
            {
                return await redisDatabase.StringSetAsync(key, serializedData, new Expiration(absoluteExpireTime.Value));
            }
            return await redisDatabase.StringSetAsync(key, serializedData);
        }

        public bool Set<T>(string key, T data, TimeSpan? absoluteExpireTime = null)
        {
            var serializedData = JsonSerializer.Serialize(data);
            if (absoluteExpireTime.HasValue)
            {
                return redisDatabase.StringSet(key, serializedData, new Expiration(absoluteExpireTime.Value));
            }
            return redisDatabase.StringSet(key, serializedData);
        }

        public async Task<bool> RemoveAsync(string key)
        {
            try
            {
                return await redisDatabase.KeyDeleteAsync(key);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Unexpected Exception");
                return false;
            }
        }

        public bool ContainsKey(string key)
        {
            return redisDatabase.KeyExists(key);
        }

        public async Task<T?> GetOrSetDataAsync<T>(string key, Func<Task<T>> factory, TimeSpan? absoluteExpireTime = null)
        {
            T? cached = await GetAsync<T>(key);
            if (cached != null)
            {
                return cached;
            }

            T value = await factory();
            await SetAsync(key, value, absoluteExpireTime);
            return value;
        }

        public T? GetOrSetData<T>(string key, Func<T> factory, TimeSpan? absoluteExpireTime = null)
        {
            T? cached = Get<T>(key);
            if (cached != null)
            {
                return cached;
            }

            T value = factory();
            Set<T>(key, value, absoluteExpireTime);
            return value;
        }
    }
}
