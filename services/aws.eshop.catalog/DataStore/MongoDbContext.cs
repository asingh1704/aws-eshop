using aws.eshop.catalog.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace aws.eshop.catalog.DataStore
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        // Define collections here
        public IMongoCollection<Product> Products => _database.GetCollection<Product>("catalog");
        public IMongoCollection<Category> Categories => _database.GetCollection<Category>("category");

        // Add more collections as needed
    }

}
