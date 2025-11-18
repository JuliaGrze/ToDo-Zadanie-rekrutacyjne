using TodoApp.Application.Dtos;

namespace TodoApp.Application.Interfaces
{
    public interface ITodoTaskService
    {
        Task<IReadOnlyList<TodoItemDto>> GetAllAsync(CancellationToken ct = default);
        Task<TodoItemDto> CreateAsync(CreateTodoItemDto dto, CancellationToken ct = default);
        Task<TodoItemDto?> MarkDoneAsync(int id, CancellationToken ct = default);
    }
}
