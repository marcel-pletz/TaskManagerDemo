using TaskManagerDemo.Domain.Todos.Aggregates;
using TaskManagerDemo.Domain.Todos.ValueObjects;
using TaskManagerDemo.Domain.Users.Aggregates;
using TaskManagerDemo.Domain.Users.ValueObjects;

namespace TaskManagerDemo.Infrastructure.Database.Configurations;

public static class DummyDataSeeder 
{
    public static void Seed(TaskManagerDbContext context)
    {
        if (context.Users.Any())
        {
            return;
        }
        
        var user1 = User.Create(
            new UserName("user1"), 
            new Email("user1@example.com"), 
            Role.User);
        
        var user2 = User.Create(
            new UserName("user2"), 
            new Email("user2@example.com"), 
            Role.User);
        
        var admin = User.Create(
            new UserName("admin"), 
            new Email("admin@example.com"), 
            Role.Administrator);
        
        context.Users.AddRange(user1, user2, admin);

        var todo1 = Todo.Create(
            new Title("Todo 1"),
            new Description("long description for user 1"), 
            user1.Id,
            DateTime.Now);
        
        var todo2 = Todo.Create(
            new Title("Todo 2"),
            new Description("long description user 2"), 
            user1.Id,
            DateTime.Now);
        
        context.Todos.AddRange(todo1, todo2);
        
        context.SaveChanges();
    }
}