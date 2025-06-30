using BasketService.Interfaces;
using BasketService.Models;
using StackExchange.Redis;
using System.Text.Json;
using IDatabase = StackExchange.Redis.IDatabase;

namespace BasketService.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {   
            var basket = await _database.StringGetAsync(BasketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public Task<bool> RemoveBasketAsync(string BasketId)
        {
            return _database.KeyDeleteAsync(BasketId);
        }



        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
        {
         bool createdOrUpdated = await _database.StringSetAsync(Basket.Id, JsonSerializer.Serialize(Basket));

            if (!createdOrUpdated)
            {
                return null;
            }
            else return await GetBasketAsync(Basket.Id);

        }
    }


}
