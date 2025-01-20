using E_commerce.core.Services.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Services.Services.Caches
{
    public class CashesService : ICacheService
    {
        private readonly IDatabase _database;
        public CashesService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<string> GetCacheKeyAsync(string cacheKey)
        {
           var cachResponse =await _database.StringGetAsync(cacheKey);
            if (cachResponse.IsNullOrEmpty) return null;
            return cachResponse.ToString();
        }

        public async Task SetCacheKeyAsync(string cacheKey, object response, TimeSpan expirtTime)
        {
            if (response is null) return;
            var option = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
           await _database.StringSetAsync(cacheKey, JsonSerializer.Serialize(response,option), expirtTime);
        }
    }
}
