using Microsoft.EntityFrameworkCore;
using AssetManagementApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // This is the fix for the circular reference issue.
        // It prevents infinite loops when serializing related objects (e.g., an Asset and its related documents)
        // by ignoring references that would cause a loop.
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the database context.
// It uses SQL Server and gets the connection string from the application's configuration.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS policy to allow the React front end to access the API.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin() // Allows requests from any origin
            .AllowAnyMethod() // Allows all HTTP methods (GET, POST, PUT, DELETE, etc.)
            .AllowAnyHeader()); // Allows all headers
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger and Swagger UI only in the development environment.
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use the CORS policy defined above.
app.UseCors("CorsPolicy");

// This line enables the serving of static files from the `wwwroot` folder.
// This is crucial for making uploaded files (documents, 2D/3D models) accessible
// via a URL.
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();