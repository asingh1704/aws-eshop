using aws.eshop.cart.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using StackExchange.Redis;
using System.Text.Json;
using StackExchange.Redis;
using System.Text.Json;

namespace aws.eshop.cart.DataStore
{
    public interface ICartStore
    {
        Task<Cart> GetCartAsync(string name);
        Task SaveCartAsync(Cart cart);
    }

    public class CartStore : ICartStore
    {
        private readonly IDatabase _redisDb;
        private readonly TimeSpan _cacheExpiry = TimeSpan.FromHours(24); // Cache expiry time

        public CartStore(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }

        public async Task<Cart> GetCartAsync(string name)
        {
            string cacheKey = GetCartCacheKey(name);
            var cachedCart = await _redisDb.StringGetAsync(cacheKey);

            if (!cachedCart.IsNullOrEmpty)
            {
                return JsonSerializer.Deserialize<Cart>(cachedCart);
            }

            // Handle cases where cart doesn't exist
            return new Cart { Name = name };
        }

        public async Task SaveCartAsync(Cart cart)
        {
            string cacheKey = GetCartCacheKey(cart.Name);
            string serializedCart = JsonSerializer.Serialize(cart);

            await _redisDb.StringSetAsync(cacheKey, serializedCart, _cacheExpiry);
        }

        private static string GetCartCacheKey(string cartName) => $"cart:{cartName}";
    }
}
