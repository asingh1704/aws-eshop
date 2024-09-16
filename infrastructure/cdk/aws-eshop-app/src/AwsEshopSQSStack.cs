using Amazon.CDK;
using Amazon.CDK.AWS.SQS;
using Constructs;

namespace AwsEshopApp
{
    public class AwsEshopSQSStack : Stack
    {
        public AwsEshopSQSStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // Create an SQS Queue
            var queue = new Queue(this, "aws-eshop-order-queue", new QueueProps
            {
                QueueName = "aws-eshop-order-queue",
                VisibilityTimeout = Duration.Seconds(30),  // Set timeout as required
                RetentionPeriod = Duration.Days(1)         // Free tier allows up to 4 days
            });
        }
    }
}
