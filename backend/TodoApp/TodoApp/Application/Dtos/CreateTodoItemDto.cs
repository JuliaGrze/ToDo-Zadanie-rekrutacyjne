using System.ComponentModel.DataAnnotations;

namespace TodoApp.Application.Dtos
{
    public class CreateTodoItemDto
    {
        [Required]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }
}
