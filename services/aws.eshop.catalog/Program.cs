using Amazon.DynamoDBv2;
using aws.eshop.catalog.DataStore;
using aws.eshop.catalog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

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


// Add authorization services
builder.Services.AddAuthorization();
// Add authentication services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://cognito-idp.us-east-1.amazonaws.com/us-east-1_tdDYq6hv5";
        //options.MetadataAddress = "https://cognito-idp.us-east-1.amazonaws.com/us-east-1_tdDYq6hv5";
        options.IncludeErrorDetails = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false, // Set to true if using Audience validation
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://cognito-idp.us-east-1.amazonaws.com/us-east-1_tdDYq6hv5" // Ensure this matches your issuer
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


