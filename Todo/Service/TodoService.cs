using Todo.Model;
using Todo.Repository;

namespace Todo.Service
{
    public class TodoService
    {
        private readonly TodoRepository todoRepository;
        public TodoService(TodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        public async Task<TodoClass> CreateTodoAsync(CreateTodoDto dto)
        {
            // Business logic could be added here (e.g., duplicate check)
            var todo = new TodoClass
            {
                Title = dto.Title,
                Description = dto.Description
            };

            return await todoRepository.AddAsync(todo);
        }
    }
}
