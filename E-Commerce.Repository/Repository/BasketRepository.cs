using E_commerce.core.Models;
using E_commerce.core.Repository.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _dataBase;

        public BasketRepository(IConnectionMultiplexer redis)
        {
          _dataBase= redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
          return await _dataBase.KeyDeleteAsync (BasketId);
               
        }

        public async Task<UserBasket?> GetBasketAsync(string BasketId)
        {
           var basket = await _dataBase.StringGetAsync (BasketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<UserBasket>(basket);
        }

        public async Task<UserBasket> UpdateBasketAsync(UserBasket basket)
        {
          var CreatedOrUpdated= await _dataBase.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket),TimeSpan.FromDays(30));
            if (CreatedOrUpdated is false) return null ;
            return await GetBasketAsync(basket.Id);
        }
    }
}
