using Amazon.SQS;
using aws.eshop.cart.DataStore;
using aws.eshop.cart.Sqs;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Services.AddSingleton<IAmazonSQS>(new AmazonSQSClient());
builder.Services.AddTransient<IQueueService, QueueService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// Add Redis services
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse(builder.Configuration["Redis:ConnectionString"], true);
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddScoped<ICartStore, CartStore>();
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
