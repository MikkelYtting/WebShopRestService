using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebShopRestService.Data;
using Microsoft.EntityFrameworkCore;
using WebShopRestService.Managers; // Ensure this namespace correctly references where your UserCredentialsManager is located.
using WebShopRestService.Interfaces; // Namespace for your interfaces
using WebShopRestService.Repositories; // Assuming this is the namespace for your repository implementations
using WebShopRestService.Configurations;

var builder = WebApplication.CreateBuilder(args);

var jwtConfigSection = builder.Configuration.GetSection("JwtConfig");
builder.Services.Configure<JwtConfig>(jwtConfigSection);
var jwtConfig = jwtConfigSection.Get<JwtConfig>(); // Get the JwtConfig instance with the bound settings.
if (string.IsNullOrEmpty(jwtConfig.Secret))
{
    throw new InvalidOperationException("JWT key is not configured properly.");
}

// Add services to the container.
builder.Services.AddControllers();

// Register the DbContext with dependency injection to access the database context throughout the application.
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbConnection")));

builder.Services.AddAutoMapper(typeof(MapperInitializer));

// Add the UserCredentialsManager to the services collection to manage user authentication tasks.
builder.Services.AddScoped<UserCredentialsManager>();

// Register additional managers here
builder.Services.AddScoped<RolesManager>();
builder.Services.AddScoped<ProductsManager>();
builder.Services.AddScoped<OrderTablesManager>();
builder.Services.AddScoped<OrderItemsManager>();
builder.Services.AddScoped<CustomersManager>();
builder.Services.AddScoped<CategoriesManager>();
builder.Services.AddScoped<AddressesManager>();

// Register the repository implementations for dependency injection
builder.Services.AddScoped<IRolesRepository, RolesRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IOrderTablesRepository, OrderTablesRepository>();
builder.Services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<IAddressesRepository, AddressesRepository>();

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret)),
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

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Enforce the use of HTTPS to secure your app by redirecting HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Use authentication and authorization middleware to ensure our API endpoints are protected.
app.UseAuthentication();
app.UseAuthorization();

// Map controllers to endpoints.
app.MapControllers();

// Run the application.
app.Run();
