using Amazon.DynamoDBv2;
using aws.eshop.catalog.DataStore;
using aws.eshop.catalog.Models;
using aws.eshop.catalog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add services to the container.
builder.Services.AddControllers();
// Add AWS DynamoDB service
builder.Services.AddAWSService<IAmazonDynamoDB>();

builder.Services.AddSingleton<IAmazonDynamoDB>(provider =>
            new AmazonDynamoDBClient()); // Configure with your settings
builder.Services.AddSingleton<IDynamoDBContextFactory, DynamoDBContextFactory>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDbContext>();

// Add authorization services
builder.Services.AddAuthorization();
// Add authentication services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://cognito-idp.us-east-1.amazonaws.com/us-east-1_IETKbHU6D";
        //options.MetadataAddress = "https://cognito-idp.us-east-1.amazonaws.com/us-east-1_IETKbHU6D";
        options.IncludeErrorDetails = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false, // Set to true if using Audience validation
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://cognito-idp.us-east-1.amazonaws.com/us-east-1_IETKbHU6D" // Ensure this matches your issuer
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                // Log or inspect the exception
                Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            }
        };
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


