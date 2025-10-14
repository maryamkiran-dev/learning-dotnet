using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Todo.Model;
namespace Todo
{
    public class AppDbContext: DbContext
    { 
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options) { }

            public DbSet<TodoClass> NewTodos { set; get; }
        }
    }

