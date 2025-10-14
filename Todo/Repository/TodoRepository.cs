using Todo.Model;
using Microsoft.EntityFrameworkCore;
namespace Todo.Repository
{
    public class TodoRepository
    {
        private readonly AppDbContext _context;
        public TodoRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<TodoClass> AddAsync(TodoClass todo)
        {
            _context.NewTodos.Add(todo);
            await _context.SaveChangesAsync();
            return todo;
        }
    }
}
