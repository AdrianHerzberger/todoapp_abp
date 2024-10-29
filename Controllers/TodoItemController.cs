using Microsoft.AspNetCore.Mvc;
using todoapp.Service.Contracts;

namespace todoapp.Controllers
{
    [Route("api/todoItems")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly IServiceManager _service;

        public TodoItemController(IServiceManager service) => _service = service;

        [HttpGet(Name = "GetAllTodoItems")]
        public async Task<IActionResult> GetAllTodoItems()
        {
            var todoItems = await _service.TodoService.GetAllTodosAsync(trackChanges: false);

            return Ok(todoItems);
        }

        [HttpGet("{id:guid}", Name = "TodoItemById")]
        public async Task<IActionResult> GetTodoAsync(Guid id)
        {
            var todoItem = await _service.TodoService.GetTodoAsync(id, trackChanges: false);
            return Ok(todoItem);
        }
    }
}
