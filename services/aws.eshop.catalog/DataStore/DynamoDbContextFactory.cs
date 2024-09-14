using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;

namespace aws.eshop.catalog.DataStore
{
    public interface IDynamoDBContextFactory
    {
        DynamoDBContext CreateContext();
    }

    public class DynamoDBContextFactory : IDynamoDBContextFactory
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;

        public DynamoDBContextFactory(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }

        public DynamoDBContext CreateContext()
        {
            return new DynamoDBContext(_dynamoDbClient);
        }
    }
}
