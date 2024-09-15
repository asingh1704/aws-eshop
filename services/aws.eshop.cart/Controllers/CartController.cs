using aws.eshop.cart.DataStore;
using aws.eshop.cart.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace aws.eshop.cart.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartStore _cartService;

        public CartController(ICartStore cartService)
        {
            _cartService = cartService;
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
    }
}