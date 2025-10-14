using CURDTodoApi;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApi(); // modern .NET 9/10 Swagger
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// --- In-memory store ---
var todos = new List<Todo>();

var app = builder.Build();

// --- Swagger UI ---
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Todo API v1");
    });
}

// --- Global Error Handling ---
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new
        {
            error = "Internal Server Error",
            details = ex.Message
        });
    }
});

//Create
app.MapPost("/todos", async (Todo dto, IValidator<Todo> validator,AppDbContext db) =>
{
    ValidationResult result = await validator.ValidateAsync(dto);
    if (!result.IsValid)
        return Results.BadRequest(result.Errors.Select(e => e.ErrorMessage));

    db.Todos.Add(dto);
    await db.SaveChangesAsync();
    return Results.Created($"/todos/{todos.Count - 1}", dto);
});
//Read All
app.MapGet("/todos", async (AppDbContext db) =>
 await db.Todos.ToListAsync()
);
//update
app.MapPut("/todos/{id}", async (int id, Todo updatedTodo, IValidator<Todo> validator, AppDbContext db) =>
    {
        var todo = await db.Todos.FindAsync(id);
        if(todo == null)
        {
            return Results.NotFound();
        }

        ValidationResult result = await validator.ValidateAsync(updatedTodo);
        if (!result.IsValid)
            return Results.BadRequest(result.Errors.Select(e => e.ErrorMessage));

        todo.Title= updatedTodo.Title;
        todo.IsCompleted= updatedTodo.IsCompleted;

       await  db.SaveChangesAsync();
        return Results.Ok(todo);

    }

);

//Delete
app.MapDelete("/todos/{id}", async (int id, AppDbContext db) =>
{
    var todo = await db.Todos.FindAsync(id);
    if(todo == null )
    {
        return Results.NotFound();
    }
    db.Todos.Remove(todo);
    await db.SaveChangesAsync();
    return Results.NoContent(); 
}
);


 app.Run();
