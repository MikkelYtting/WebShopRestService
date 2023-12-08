using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebShopRestService.Data;
using Microsoft.EntityFrameworkCore;
using WebShopRestService.Managers; // Ensure this namespace correctly references where your UserCredentialsManager is located.

var builder = WebApplication.CreateBuilder(args);

// Read the JwtKey from configuration, which should be securely stored either in appsettings.json or an environment variable.
var jwtKey = builder.Configuration["JwtKey"];
if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException("JWT key is not configured properly.");
}

// Add services to the container.
builder.Services.AddControllers();

// Register the DbContext with dependency injection to access the database context throughout the application.
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbConnection")));

// Add the UserCredentialsManager to the services collection to manage user authentication tasks.
builder.Services.AddScoped<UserCredentialsManager>();

// Add Authentication services and configure JWT Bearer options.
// This sets up the application to validate JWT tokens on incoming requests for secure endpoints.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Ensure the signing key is valid and is trusted by the server.
            ValidateIssuerSigningKey = true,
            // The signing key is derived from the secret stored in configuration.
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            // These settings relax the requirements on the token issuer and audience for simplicity.
            // In a production scenario, you may want to validate the issuer and audience for extra security.
            ValidateIssuer = false,
            ValidateAudience = false,
            // Reduce the clock skew allowed between the server and token issuer to zero.
            // This means tokens will expire exactly at the time specified in the token.
            ClockSkew = TimeSpan.Zero
        };
    });

// Configure Swagger to help with API documentation and testing.
// Swagger provides a UI to see all the endpoints and their respective request/response formats.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Provide a detailed error page in development environments.
    app.UseDeveloperExceptionPage();
    // Serve Swagger as a JSON endpoint.
    app.UseSwagger();
    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
    app.UseSwaggerUI();
}

// Enforce the use of HTTPS to secure your app by redirecting HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Use authentication and authorization middleware to ensure our API endpoints are protected.
app.UseAuthentication();
app.UseAuthorization();

// Map controllers to endpoints.
app.MapControllers();

// Run the application.
app.Run();
