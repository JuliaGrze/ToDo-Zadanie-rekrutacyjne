using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Dtos;
using TodoApp.Application.Interfaces;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoTaskService _service;

        public TodosController(ITodoTaskService service)
        {
            _service = service;
        }

        // GET /api/todos
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TodoItemDto>>> GetAll(CancellationToken ct)
        {
            var tasks = await _service.GetAllAsync(ct);
            return Ok(tasks);
        }

        // POST /api/todos
        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> Create([FromBody] CreateTodoItemDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var created = await _service.CreateAsync(dto, ct);

            return CreatedAtAction(nameof(GetAll), new { }, created);
        }

        //PUT /api/{id}/todos/toggle-done"
        [HttpPut("{id:int}/toggle-done")]
        public async Task<ActionResult<TodoItemDto>> ToggleDone(int id, CancellationToken ct)
        {
            var updated = await _service.ToggleDoneAsync(id, ct);
            if (updated is null)
                return NotFound();

            return Ok(updated);
        }

    }
}
