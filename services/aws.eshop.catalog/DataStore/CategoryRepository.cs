using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using aws.eshop.catalog.Models;

namespace aws.eshop.catalog.DataStore
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryByIdAsync(string id);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DynamoDBContext _context;
        public CategoryRepository(IDynamoDBContextFactory contextFactory)
        {
            _context = contextFactory.CreateContext();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var conditions = new List<ScanCondition>();
            var search = _context.ScanAsync<Category>(conditions);
            return await search.GetRemainingAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(string id)
        {
            return await _context.LoadAsync<Category>(id);
        }
    }
}
