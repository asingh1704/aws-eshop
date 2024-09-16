namespace aws.eshop.order.Sqs
{
    using Amazon.SQS;
    using Amazon.SQS.Model;
    using aws.eshop.order.Models;
    using aws.eshop.order.Service;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System.Text.Json.Serialization;
    using System.Threading;
    using System.Threading.Tasks;

    public class SqsConsumerService : BackgroundService
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly IOrderService _orderService;
        public SqsConsumerService(IAmazonSQS sqsClient, IOrderService orderService)
        {
            _sqsClient = sqsClient;
            _orderService = orderService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var response = await _sqsClient.ReceiveMessageAsync(new ReceiveMessageRequest
                {
                    QueueUrl = "https://sqs.us-east-1.amazonaws.com/533267006776/aws-eshop-order-queue",
                    MaxNumberOfMessages = 1,
                    WaitTimeSeconds = 20 // Long polling
                }, stoppingToken);

                foreach (var message in response.Messages)
                {
                    // Process the message

                    var cart = JsonConvert.DeserializeObject<Cart>(message.Body);

                    await _sqsClient.DeleteMessageAsync(new DeleteMessageRequest
                    {
                        QueueUrl = "https://sqs.us-east-1.amazonaws.com/533267006776/aws-eshop-order-queue",
                        ReceiptHandle = message.ReceiptHandle
                    });
                    await _orderService.CreateOrder(cart);
                }
            }
        }
    }


}
