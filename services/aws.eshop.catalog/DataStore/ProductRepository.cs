using aws.eshop.catalog.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace aws.eshop.catalog.DataStore
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(string id);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryId);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly MongoDbContext _dbContext;

        public ProductRepository(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.Find(_ => true).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _dbContext.Products.Find(Builders<Product>.Filter.Eq("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Category, category);
            return await _dbContext.Products.Find(filter).ToListAsync();
        }
    }
}
