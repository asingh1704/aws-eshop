using aws.eshop.catalog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapGet("/catalog", () =>
{
    var catalog = Enumerable.Range(1, 5).Select(index =>
        new Catalog
        {
            Name = "Nike Dunk",
            Qty = 10,
            Size = index
        })
        .ToArray();
    return catalog;
})
.WithName("GetCatalog")
.RequireAuthorization();

// Use authentication
app.UseAuthentication();
app.UseAuthorization();
app.Run();


