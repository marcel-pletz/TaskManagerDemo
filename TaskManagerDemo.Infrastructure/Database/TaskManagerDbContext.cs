using Microsoft.EntityFrameworkCore;
using TaskManagerDemo.Domain.Users.Aggregates;

namespace TaskManagerDemo.Infrastructure.Database;

public sealed class TaskManagerDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagerDbContext).Assembly);
    }
}