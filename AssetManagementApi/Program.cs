// File: AssetManagementApi/Program.cs

using AssetManagementApi.Data; // For ApplicationDbContext
using Microsoft.EntityFrameworkCore; // For UseSqlServer extension method

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Controllers service
builder.Services.AddControllers();

// Configure Swagger/OpenAPI for API documentation (optional but very useful)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure SQL Server DbContext
// This registers ApplicationDbContext with the dependency injection container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure CORS (Cross-Origin Resource Sharing)
// This allows your React frontend (running on a different port/domain) to make requests to this API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", // Name of your CORS policy
        builder => builder.WithOrigins("http://localhost:3000") // IMPORTANT: Replace with your React app's URL
                          .AllowAnyHeader()    // Allows any headers in the request
                          .AllowAnyMethod());   // Allows any HTTP methods (GET, POST, PUT, DELETE, etc.)
});


var app = builder.Build();

// Configure the HTTP request pipeline.
// In Development environment, use Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP requests to HTTPS (good practice for security)
app.UseHttpsRedirection();

// Use the CORS policy you defined
app.UseCors("AllowSpecificOrigin"); // IMPORTANT: This must be before app.UseAuthorization(); and app.MapControllers();

// Enable authorization (if you add authentication later)
app.UseAuthorization();

// Map controller routes
app.MapControllers();

app.Run();