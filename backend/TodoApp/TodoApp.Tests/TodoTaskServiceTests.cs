using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Application.Dtos;
using TodoApp.Application.Services;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Data;
using Xunit;

namespace TodoApp.Tests.Application.Services
{
    public class TodoTaskServiceTests
    {
        private static TodoDbContext CreateDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new TodoDbContext(options);
        }

        private static TodoTaskService CreateService(TodoDbContext context)
        {
            return new TodoTaskService(context);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateTask_WithTrimmedValues_AndIsDoneFalse()
        {
            // Arrange
            using var context = CreateDbContext(nameof(CreateAsync_ShouldCreateTask_WithTrimmedValues_AndIsDoneFalse));
            var service = CreateService(context);

            var dto = new CreateTodoItemDto
            {
                Title = "  Testowe zadanie  ",
                Description = "  Opis zadania  "
            };

            // Act
            var result = await service.CreateAsync(dto, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
            Assert.Equal("Testowe zadanie", result.Title);
            Assert.Equal("Opis zadania", result.Description);
            Assert.False(result.IsDone);

            var entityInDb = await context.TodoTasks.SingleAsync();
            Assert.Equal(result.Id, entityInDb.Id);
            Assert.Equal("Testowe zadanie", entityInDb.Title);
            Assert.Equal("Opis zadania", entityInDb.Description);
            Assert.False(entityInDb.IsDone);
        }

        [Fact]
        public async Task CreateAsync_ShouldSetDescriptionNull_WhenDescriptionIsWhitespace()
        {
            // Arrange
            using var context = CreateDbContext(nameof(CreateAsync_ShouldSetDescriptionNull_WhenDescriptionIsWhitespace));
            var service = CreateService(context);

            var dto = new CreateTodoItemDto
            {
                Title = "Zadanie",
                Description = "   " 
            };

            // Act
            var result = await service.CreateAsync(dto, CancellationToken.None);

            // Assert
            Assert.Null(result.Description);

            var entityInDb = await context.TodoTasks.SingleAsync();
            Assert.Null(entityInDb.Description);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnTasksOrdered_ByIsDoneDesc_ThenIdAsc()
        {
            // Arrange
            using var context = CreateDbContext(nameof(GetAllAsync_ShouldReturnTasksOrdered_ByIsDoneDesc_ThenIdAsc));

            context.TodoTasks.AddRange(
              new TodoTask { Title = "Zadanie 1", IsDone = false },
              new TodoTask { Title = "Zadanie 2", IsDone = true },
              new TodoTask { Title = "Zadanie 3", IsDone = true }
            );
            await context.SaveChangesAsync();

            var service = CreateService(context);

            // Act
            var result = await service.GetAllAsync(CancellationToken.None);

            // Assert
            Assert.Equal(3, result.Count);

            // najpierw wykonane (true), potem niewykonane (false)
            Assert.True(result[0].IsDone);
            Assert.True(result[1].IsDone);
            Assert.False(result[2].IsDone);

            var doneIds = result.Where(r => r.IsDone).Select(r => r.Id).ToList();
            var sortedDoneIds = doneIds.OrderBy(id => id).ToList();
            Assert.Equal(sortedDoneIds, doneIds);
        }

        [Fact]
        public async Task ToggleDoneAsync_ShouldToggleAndReturnDto_WhenEntityExists()
        {
            // Arrange
            using var context = CreateDbContext(nameof(ToggleDoneAsync_ShouldToggleAndReturnDto_WhenEntityExists));

            var task = new TodoTask
            {
                Title = "Zadanie do prze³¹czenia",
                Description = null,
                IsDone = false
            };

            context.TodoTasks.Add(task);
            await context.SaveChangesAsync();

            var service = CreateService(context);

            // Act
            var result = await service.ToggleDoneAsync(task.Id, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result!.IsDone);

            var entityInDb = await context.TodoTasks.FindAsync(task.Id);
            Assert.NotNull(entityInDb);
            Assert.True(entityInDb!.IsDone);
        }

        [Fact]
        public async Task ToggleDoneAsync_ShouldReturnNull_WhenEntityDoesNotExist()
        {
            // Arrange
            using var context = CreateDbContext(nameof(ToggleDoneAsync_ShouldReturnNull_WhenEntityDoesNotExist));
            var service = CreateService(context);

            // Act
            var result = await service.ToggleDoneAsync(999, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
