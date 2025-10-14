using CURDTodoApi;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
 
var builder = WebApplication.CreateBuilder(args);
var key = "fdhjfhjdnvjchfjdhfuh8re8r9w8rw8375hf84wu9u38hfi";
//Configure Authentication + JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "CURDTodoApi", Version = "v1" });

    // 🔒 Add JWT support to Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter your JWT token below (with 'Bearer ' prefix). Example: Bearer eyJhbGciOiJIUzI1NiIs..."
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthorization();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApi(); // modern .NET 9/10 Swagger
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// --- In-memory store ---
var todos = new List<Todo>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();



// --- Swagger UI ---
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Todo API v1");
    });
}
 

app.MapPost("/login", (UserLogin user) =>
{
    if (user.Username == "admin" && user.Password == "1234")
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return Results.Ok(new { token = jwt });
    }

    return Results.Unauthorized();
});
 
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
}).RequireAuthorization(); ;
//Read All
app.MapGet("/todos", async (AppDbContext db) =>
 await db.Todos.ToListAsync()
).RequireAuthorization(); ;
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

).RequireAuthorization(); ;

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
).RequireAuthorization(); ;


 app.Run();
