using TaskManagerDemo.Domain.Todos.ValueObjects;
using TaskManagerDemo.Domain.Users.Aggregates;

namespace TaskManagerDemo.Domain.Todos.Aggregates;

public sealed class Todo : IAggregateRoot
{
    public TodoId Id { get; init; }
    
    public Title Title { get; init; }
    
    public Description Description { get; init; }
    
    public UserId OwnerId { get; init; }

    private Todo(TodoId id, Title title, Description description, UserId ownerId)
    {
        Id = id;
        Title = title;
        Description = description;
        OwnerId = ownerId;
    }
    
    public static Todo Create(Title title, Description description, UserId ownerId)
    {
        var id = new TodoId(Guid.NewGuid());
        return new Todo(id, title, description, ownerId);
    }
}