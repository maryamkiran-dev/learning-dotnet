using CQRS.Model;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }

    }
}
