using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Model;
using Todo.Service;

namespace Todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoService _service; 

        public TodoController(TodoService service )
        {
            _service = service; 
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTodoDto dto)
        { 

            var result = await _service.CreateTodoAsync(dto);
            return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
        }
    }
}
