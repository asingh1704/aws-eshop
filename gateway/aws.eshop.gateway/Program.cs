using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()  // Allow all origins, you can specify your frontend URL instead.
                   .AllowAnyMethod()  // Allow all HTTP methods (GET, POST, etc.).
                   .AllowAnyHeader(); // Allow any headers (Authorization, Content-Type, etc.).
        });
});

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

app.UseCors("AllowAllOrigins"); // Enable CORS globally

// Use authentication
app.UseAuthentication();
app.UseAuthorization();


app.MapReverseProxy().RequireAuthorization("ApiPolicy");
app.Run();