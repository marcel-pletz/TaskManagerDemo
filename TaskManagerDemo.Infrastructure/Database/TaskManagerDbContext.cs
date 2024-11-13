using Microsoft.EntityFrameworkCore;
using TaskManagerDemo.Domain.Todos.Aggregates;
using TaskManagerDemo.Domain.Users.Aggregates;

namespace TaskManagerDemo.Infrastructure.Database;

public sealed class TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : DbContext(options)
{
    internal DbSet<User> Users => Set<User>();

    internal DbSet<Todo> Todos => Set<Todo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagerDbContext).Assembly);
    }
}