using Amazon.DynamoDBv2.DataModel;

namespace aws.eshop.catalog.Models
{
    [DynamoDBTable("Category")]
    public class Category
    {
        [DynamoDBHashKey]  // Partition key
        public string CategoryId { get; set; }

        [DynamoDBProperty]
        public string CategoryName { get; set; }
    }
}
