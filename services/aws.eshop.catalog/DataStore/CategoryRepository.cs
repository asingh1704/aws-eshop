using aws.eshop.catalog.Models;
using MongoDB.Driver;

namespace aws.eshop.catalog.DataStore
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MongoDbContext _dbContext;

        public CategoryRepository(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories.Find(_ => true).ToListAsync();
        }
    }
}
