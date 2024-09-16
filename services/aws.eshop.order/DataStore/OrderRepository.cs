using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using aws.eshop.order.Models;

namespace aws.eshop.order.DataStore
{
    public interface IOrderRepository
    {
        Task SaveOrderAsync(Order order);
        Task<List<Order>> GetOrdersByUsernameAsync(string username);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly DynamoDBContext _context;

        public OrderRepository(IDynamoDBContextFactory contextFactory)
        {
            _context = contextFactory.CreateContext();
        }

        public async Task SaveOrderAsync(Order order)
        {
            await _context.SaveAsync(order);
        }

        public async Task<List<Order>> GetOrdersByUsernameAsync(string username)
        {
            var conditions = new List<ScanCondition>
        {
            new ScanCondition("Username", ScanOperator.Equal, username)
        };

            var orders = await _context.ScanAsync<Order>(conditions).GetRemainingAsync();
            return orders;
        }
    }
}
