using aws.eshop.order.Models;
using MongoDB.Driver;

namespace aws.eshop.order.DataStore
{
    public interface IOrderRepository
    {
        Task SaveOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersByUsernameAsync(string username);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly MongoDbContext _dbContext;

        public OrderRepository(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveOrderAsync(Order order)
        {
            await _dbContext.Orders.InsertOneAsync(order);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUsernameAsync(string username)
        {
            var filter = Builders<Order>.Filter.Eq(p => p.Username, username);
            return await _dbContext.Orders.Find(filter).ToListAsync();
        }
    }
}
