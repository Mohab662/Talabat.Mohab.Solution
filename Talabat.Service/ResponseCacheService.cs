using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Services.Contract;

namespace Talabat.Service
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDatabase _database;

        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan expireTime)
        {
            if (response is null) return;


            var option = new JsonSerializerOptions() 
            {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            
            };
            var serializedResponse=JsonSerializer.Serialize(response,option);

           await _database.StringSetAsync(cacheKey, serializedResponse, expireTime);
        }

        public async Task<string?> GetCachedResponseAsync(string cacheKey)
        {
            var cacheResponse = await _database.StringGetAsync(cacheKey);

            if(cacheResponse.IsNullOrEmpty) return null;

            return cacheResponse;
        }
    }
}
