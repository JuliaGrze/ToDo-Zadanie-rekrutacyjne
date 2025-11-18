using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Data.Configurations;

namespace TodoApp.Infrastructure.Data
{
    public class TodoDbContext : DbContext
    {
        public DbSet<TodoTask> TodoTasks => Set<TodoTask>();

        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoTaskConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
