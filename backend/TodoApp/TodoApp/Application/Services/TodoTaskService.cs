using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Dtos;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Data;

namespace TodoApp.Application.Services
{
    public class TodoTaskService : ITodoTaskService
    {
        private readonly TodoDbContext _db;

        public TodoTaskService(TodoDbContext db)
        {
            _db = db;
        }

        public async Task<TodoItemDto> CreateAsync(CreateTodoItemDto dto, CancellationToken ct = default)
        {
            var entity = new TodoTask
            {
                Title = dto.Title.Trim(),
                Description = string.IsNullOrWhiteSpace(dto.Description)
                ? null
                : dto.Description.Trim(),
                IsDone = false
            };

            _db.TodoTasks.Add(entity);
            await _db.SaveChangesAsync(ct);

            return new TodoItemDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                IsDone = entity.IsDone
            };
        }

        public async Task<IReadOnlyList<TodoItemDto>> GetAllAsync(CancellationToken ct = default)
        {
            return await _db.TodoTasks
                .AsNoTracking()
                .OrderByDescending(t => t.IsDone)
                .ThenBy(t => t.Id)
                .Select(t => new TodoItemDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    IsDone = t.IsDone
                })
                .ToListAsync(); 
        }

        public async Task<TodoItemDto?> ToggleDoneAsync(int id, CancellationToken ct = default)
        {
            var entity = await _db.TodoTasks.FirstOrDefaultAsync(t => t.Id == id, ct);
            if (entity is null)
                return null;

            entity.IsDone = !entity.IsDone;
            await _db.SaveChangesAsync(ct);

            return new TodoItemDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                IsDone = entity.IsDone
            };
        }
    }
}

