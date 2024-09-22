using aws.eshop.order.DataStore;
using aws.eshop.order.Models;
using Microsoft.AspNetCore.Mvc;
namespace aws.eshop.order.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _dynamoDbService;

        public OrdersController(IOrderRepository dynamoDbService)
        {
            _dynamoDbService = dynamoDbService;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUsername(string username)
        {
            var orders = await _dynamoDbService.GetOrdersByUsernameAsync(username);

            if (orders == null || orders.Count() == 0)
            {
                return NotFound();
            }

            return Ok(orders);
        }
    }

}
