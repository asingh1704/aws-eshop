using aws.eshop.cart.DataStore;
using aws.eshop.cart.Models;
using aws.eshop.cart.Sqs;
using Microsoft.AspNetCore.Mvc;

namespace aws.eshop.cart.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartStore _cartService;
        private readonly IQueueService _queueService;

        public CartController(ICartStore cartService, IQueueService queueService)
        {
            _cartService = cartService;
            _queueService = queueService;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCart(string name)
        {
            var cart = await _cartService.GetCartAsync(name);
            if (cart == null)
            {
                return NotFound("Cart not found");
            }
            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCart([FromBody] Cart cart)
        {
            if (cart == null || cart.Items.Count == 0)
            {
                return BadRequest("Invalid cart data");
            }

            await _cartService.SaveCartAsync(cart);
            return Ok();
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] Cart cart)
        {
            await _queueService.SendMessageAsync(cart);

            return Ok(new { Message = "Checkout completed successfully" });
        }
    }
}