﻿using aws.eshop.order.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace aws.eshop.order.DataStore
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
        public IMongoCollection<Order> Orders => _database.GetCollection<Order>("order");
        // Add more collections as needed
    }

    public class MongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
