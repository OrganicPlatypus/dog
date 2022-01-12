using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace domesticOrganizationGuru.Redis
{
    public interface IRedisProvider
    {
        Task<T> GetFromStorage<T>(string key) where T: class;
        Task SetStorageRecord<T>(string key, T value, TimeSpan? absoluteExpirationTime = null);
    }
}