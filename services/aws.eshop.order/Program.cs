using Amazon.DynamoDBv2;
using Amazon.SQS;
using aws.eshop.order.DataStore;
using aws.eshop.order.Service;
using aws.eshop.order.Sqs;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// Add AWS DynamoDB service
builder.Services.AddTransient<IOrderRepository,OrderRepository>();
builder.Services.AddTransient<IOrderService,OrderService>();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<IAmazonDynamoDB>(provider =>
            new AmazonDynamoDBClient()); // Configure with your settings
builder.Services.AddSingleton<IAmazonSQS>(new AmazonSQSClient());

builder.Services.AddHostedService<SqsConsumerService>(provider =>
{
    var sqsClient = provider.GetRequiredService<IAmazonSQS>();
    var orderService = provider.GetRequiredService<IOrderService>();
    
    return new SqsConsumerService(sqsClient, orderService);
});

var app = builder.Build();

app.UseRouting();
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


// Use authentication
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
