using Amazon.DynamoDBv2;
using aws.eshop.catalog.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace aws.eshop.catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductService _productService;

        public CatalogController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("products")]
        public async Task<IActionResult> Products()
        {
            try
            {
                var response = await _productService.GetAllProductsAsync();
                return Ok(response);
            }
            catch (AmazonDynamoDBException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"DynamoDB Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpGet("categories")]
        public async Task<IActionResult> Categories()
        {
            try
            {
                var response = await _productService.GetAllCategoriesAsync();
                return Ok(response);
            }
            catch (AmazonDynamoDBException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"DynamoDB Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}
