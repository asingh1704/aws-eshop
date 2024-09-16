using aws.eshop.order.DataStore;
using aws.eshop.order.Models;
using Newtonsoft.Json;

namespace aws.eshop.order.Service
{
    public interface IOrderService
    {
        Task CreateOrder(Cart cart);
    }
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _dynamoDbService;

        public OrderService(IOrderRepository dynamoDbService)
        {
            _dynamoDbService = dynamoDbService;
        }

        public async Task CreateOrder(Cart cart)
        {
            var order = new Order {
                Items = JsonConvert.SerializeObject(cart.Items),
                OrderId = Guid.NewGuid().ToString(),
                Username = cart.Name
            };

            await _dynamoDbService.SaveOrderAsync(order);
        }
    }
}
