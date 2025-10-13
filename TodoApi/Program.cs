using FluentValidation; 
using FluentValidation.Results;   
using TodoApi;

var builder = WebApplication.CreateBuilder(args);

// --- Add Services ---
builder.Services.AddEndpointsApiExplorer(); 


// Register validators
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// --- In-memory data store ---
var todos = new List<Todo>();

var app = builder.Build();

// --- Error Handling ---
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
   

// --- CRUD Endpoints ---

// Create
app.MapPost("/todos", async (Todo dto, IValidator<Todo> validator) =>
{
    ValidationResult result = await validator.ValidateAsync(dto);
    if (!result.IsValid)
        return Results.BadRequest(result.Errors.Select(e => e.ErrorMessage));

    todos.Add(dto);
    return Results.Created($"/todos/{todos.Count - 1}", dto);
});