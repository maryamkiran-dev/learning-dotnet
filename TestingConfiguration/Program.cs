using Serilog;
using TestingConfiguration.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
// 1️⃣ Serilog setup
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// 2️⃣ Register services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Required by Swagger
         // Registers ISwaggerProvider

builder.Services.AddSwaggerGen();

// 3️⃣ Build app
var app = builder.Build();

// 4️⃣ Use Swagger (AFTER building, BEFORE running)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Optional: redirect / → /swagger
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

// 5️⃣ Map endpoints and run
app.MapControllers();
app.Run();
