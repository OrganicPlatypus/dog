using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace domesticOrganizationGuru.Redis.Implementation
{
    public class RedisProvider : IRedisProvider
    {
        private readonly IDatabase _database;

        public RedisProvider( 
            IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<T> GetFromStorage<T>(string key) where T: class
        {
            var asdasd = typeof(T);
            var asdasdasdafs = typeof(T).UnderlyingSystemType;
            var cachedUsers = await _database.StringGetAsync(key);
            var aaaaa = cachedUsers.ToString();

            bool hasValue = cachedUsers.HasValue;
            return cachedUsers != hasValue ? default(T) : JsonSerializer.Deserialize<T>(cachedUsers);
        }

        public async Task SetStorageRecord<T>(string key, T value, TimeSpan? absoluteExpirationTime = null)
        {

            var jsonData = JsonSerializer.Serialize(value);
            TimeSpan expirationTime = absoluteExpirationTime ?? TimeSpan.FromMinutes(60);
            await _database.StringSetAsync(key, jsonData, expirationTime);
        }

        //public async Task ClearCache(string key)
        //{
        //    await _cache.RemoveAsync(key);
        //}
    }
}