using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using aws.eshop.catalog.Models;

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
        private readonly DynamoDBContext _context;

        public ProductRepository(IDynamoDBContextFactory contextFactory)
        {
            _context = contextFactory.CreateContext();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var search = _context.ScanAsync<Product>(new List<ScanCondition>());
            return await search.GetRemainingAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _context.LoadAsync<Product>(id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryId)
        {
            var query = new QueryOperationConfig
            {
                IndexName = "CategoryIndex",
                KeyExpression = new Expression
                {
                    ExpressionStatement = "CategoryId = :v_categoryId",
                    ExpressionAttributeValues = new Dictionary<string, DynamoDBEntry>
                {
                    { ":v_categoryId", categoryId }
                }
                }
            };

            var search = _context.FromQueryAsync<Product>(query);
            return await search.GetRemainingAsync();
        }
    }
}
