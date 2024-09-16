using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Threading.Tasks;
using aws.eshop.cart.Models;

namespace aws.eshop.cart.Sqs
{
    public interface IQueueService
    {
        Task SendMessageAsync(Cart checkoutEvent);
    }
    public class QueueService : IQueueService
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly string _queueUrl;

        public QueueService(IAmazonSQS sqsClient, IConfiguration configuration)
        {
            _sqsClient = sqsClient;
            _queueUrl = "https://sqs.us-east-1.amazonaws.com/533267006776/aws-eshop-order-queue";
        }

        public async Task SendMessageAsync(Cart checkoutEvent)
        {
            // Serialize the message object to JSON
            var messageBody = JsonSerializer.Serialize(checkoutEvent);

            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = messageBody
            };

            await _sqsClient.SendMessageAsync(sendMessageRequest);
        }
    }
}



