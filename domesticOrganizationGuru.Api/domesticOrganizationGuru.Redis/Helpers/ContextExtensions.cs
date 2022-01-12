using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace domesticOrganizationGuru.Redis.Helpers
{
    public static class ContextExtensions
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache storage,
            string recordId,
            T data,
            TimeSpan? absoluteExpirationTime = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpirationTime ?? TimeSpan.FromMinutes(60)
            };

            var jsonData = JsonSerializer.Serialize(data);
            
            await storage.SetStringAsync(recordId, jsonData, options);
        }
         
        public static async Task<T> GetRecordAsync<T>(this IDistributedCache storage, string recordId)
        {

            var jsonData = await storage.GetStringAsync(recordId);

            if(jsonData is null)
                {
                    return default(T);
                };

           return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
