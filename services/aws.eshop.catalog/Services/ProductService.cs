using aws.eshop.catalog.DataStore;
using aws.eshop.catalog.Models;

namespace aws.eshop.catalog.Services
{
    public interface IProductService
    {
        Task<Product> GetProductDetailsAsync(string productId);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryId);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Product> GetProductDetailsAsync(string productId)
        {
            return await _productRepository.GetProductByIdAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryId)
        {
            return await _productRepository.GetProductsByCategoryAsync(categoryId);
        }

       

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }
    }

}
