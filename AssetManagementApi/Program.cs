// File: AssetManagementApi/Program.cs

using AssetManagementApi.Data; // For ApplicationDbContext
using Microsoft.EntityFrameworkCore; // For UseSqlServer extension method
using Microsoft.Extensions.FileProviders; // For PhysicalFileProvider
using Microsoft.AspNetCore.Hosting; // For IWebHostEnvironment (used by app.Environment)
using AssetManagementApi.Filters;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Add Controllers service with JSON options
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

// Configure Swagger/OpenAPI for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<SwaggerFileOperationFilter>();
});

// Configure CORS Policy (Consolidated into one section)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policyBuilder =>
        {
            policyBuilder.WithOrigins("http://localhost:5173", "http://localhost:3000") 
                       .AllowAnyHeader()
                       .AllowAnyMethod();
        });
});

// Configure SQL Server DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build(); // <-- Application build point

// Configure the HTTP request pipeline (Middleware configured AFTER app.Build() and BEFORE app.Run())

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // HTTPS redirection usually comes after Swagger if Swagger is to be served via HTTP
    // app.UseHttpsRedirection(); 
}
else
{
    // In production, enforce HTTPS
    app.UseHttpsRedirection();
}

// --- STATIC FILES CONFIGURATION ---
// Get the application's base directory (e.g., C:\...\AssetManagementApi)
string contentRootPath = app.Environment.ContentRootPath;

// Construct the path to AssetManagementStorage assuming it's a sibling folder
// of AssetManagementApi (e.g., C:\...\AssetManagementStorage)
string staticFilesPath = Path.Combine(contentRootPath, "..", "AssetManagementStorage");

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
        // Optionally re-throw or handle more robustly if directory creation is critical for startup
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
    RequestPath = "/StaticFiles" // This is the URL prefix for accessing files (e.g., http://localhost:5062/StaticFiles/yourfile.pdf)
});

// Use Routing middleware (must come before UseCors and UseAuthorization if using endpoint routing)
app.UseRouting();

// Use the CORS policy you defined (must come after UseRouting() and before UseAuthorization())
app.UseCors("AllowSpecificOrigin");

// Use Authorization middleware
app.UseAuthorization();

// Map controller routes (must come after UseRouting and UseAuthorization)
app.MapControllers();

app.Run();