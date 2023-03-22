using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _cache;

        public BasketRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task DeleteBasketAsync(string username)
        {
            await _cache.RemoveAsync(username);
        }

        public async Task<ShoppingCart> GetBasketAsync(string username)
        {
            var json = await _cache.GetStringAsync(username);

            if(string.IsNullOrWhiteSpace(json)) 
                return default;

            return JsonSerializer.Deserialize<ShoppingCart>(json);
        }

        public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart cart)
        {
            await _cache.SetStringAsync(cart.Username, JsonSerializer.Serialize(cart));

            return cart;
        }
    }
}
