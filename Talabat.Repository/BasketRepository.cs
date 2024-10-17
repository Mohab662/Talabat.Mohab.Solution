using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text.Json;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetCutomerBasketAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);

            return basketId.IsNullOrEmpty() ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var createdOrUpdatedBasket = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if (createdOrUpdatedBasket is false)
            {
                return null;
            }
            return await GetCutomerBasketAsync(basket.Id);
        }
    }
}
