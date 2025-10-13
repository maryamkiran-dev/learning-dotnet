using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// ✅ Add Swagger / OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Product API",
        Version = "v1",
        Description = "A simple API for managing products"
    });
});

// Example: Scoped service registration
builder.Services.AddScoped<DTO.Model.ProductDb>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // ✅ Serve OpenAPI JSON
    app.UseSwagger();

    // ✅ Serve Swagger UI
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API v1");
        c.RoutePrefix = string.Empty; // Swagger UI will open at root URL (http://localhost:5000)
    });
}

app.MapControllers();
app.Run();
