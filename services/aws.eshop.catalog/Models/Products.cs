using Amazon.DynamoDBv2.DataModel;

namespace aws.eshop.catalog.Models
{
    [DynamoDBTable("Products")]
    public class Product
    {
        [DynamoDBHashKey]
        public string ProductId { get; set; }

        [DynamoDBProperty]
        public string CategoryId { get; set; }

        [DynamoDBProperty]
        public string Brand { get; set; }

        [DynamoDBProperty]
        public string Discription { get; set; }

        [DynamoDBProperty]
        public string ProductName { get; set; }

        [DynamoDBProperty]
        public string ImageUrl { get; set; }

        [DynamoDBProperty]
        public decimal Qty { get; set; }

        [DynamoDBProperty]
        public decimal Price { get; set; }
    }
}
