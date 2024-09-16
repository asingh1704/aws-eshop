using Amazon.DynamoDBv2.DataModel;

namespace aws.eshop.order.Models
{
    [DynamoDBTable("Order")]
    public class Order
    {
        [DynamoDBHashKey] // Partition key
        public string OrderId { get; set; }

        [DynamoDBProperty]
        public string Username { get; set; }

        [DynamoDBProperty]
        public string Items { get; set; }
    }
}
