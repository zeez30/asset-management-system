// File: AssetManagementApi/Program.cs

using AssetManagementApi.Data; // For ApplicationDbContext
using Microsoft.EntityFrameworkCore; // For UseSqlServer extension method
using Microsoft.Extensions.FileProviders; // For PhysicalFileProvider
using Microsoft.AspNetCore.Hosting; // For IWebHostEnvironment (used by app.Environment)

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Controllers service
builder.Services.AddControllers();

// Configure Swagger/OpenAPI for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure SQL Server DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", // Name of your CORS policy
        builder => builder.WithOrigins("http://localhost:3000") // IMPORTANT: Replace with your React app's URL
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
// In Development environment, use Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// --- STATIC FILES CONFIGURATION ---
// Get the application's base directory (e.g., C:\...\AssetManagementApi)
string contentRootPath = app.Environment.ContentRootPath;

// Construct the path to AssetManagementStorage assuming it's a sibling folder
// of AssetManagementApi (e.g., C:\...\AssetManagementStorage)
string staticFilesPath = Path.Combine(contentRootPath, "..", "AssetManagementStorage");

// Ensure the directory exists. Create it if it doesn't.
if (!Directory.Exists(staticFilesPath))
{
    try
    {
        Directory.CreateDirectory(staticFilesPath);
        Console.WriteLine($"[INFO] Successfully created static files directory: {staticFilesPath}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[ERROR] Failed to create static files directory '{staticFilesPath}': {ex.Message}");
        // You might want to log the full exception details here in a real application
    }
}
else
{
    Console.WriteLine($"[INFO] Static files directory already exists: {staticFilesPath}");
}

// Enable static file serving from the specified path
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(staticFilesPath),
    RequestPath = "/StaticFiles" // This is the URL prefix (e.g., http://localhost:5062/StaticFiles/my_doc.pdf)
});
// --- END STATIC FILES CONFIGURATION ---

// Use the CORS policy you defined
app.UseCors("AllowSpecificOrigin"); // IMPORTANT: This must be before app.UseAuthorization();

app.UseAuthorization();

// Map controller routes
app.MapControllers();

app.Run();