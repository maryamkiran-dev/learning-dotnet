
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Todo;
using Todo.Model;
using Todo.Repository;
using Todo.Service;  


var builder = WebApplication.CreateBuilder(args);

// Add DbContext (InMemory for simplicity)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



// Add services
builder.Services.AddScoped<TodoRepository>();
builder.Services.AddScoped<TodoService>(); 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
