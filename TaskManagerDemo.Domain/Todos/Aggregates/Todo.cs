using TaskManagerDemo.Domain.Todos.ValueObjects;
using TaskManagerDemo.Domain.Users.Aggregates;

namespace TaskManagerDemo.Domain.Todos.Aggregates;

public sealed class Todo : IAggregateRoot
{
    public TodoId Id { get; init; }
    
    public Title Title { get; init; }
    
    public Description Description { get; init; }
    
    public UserId OwnerId { get; init; }

    public Status Status { get; private set; } = Status.Todo;
    
    public DateTime CreationDateTime { get; private init; }
    
    public DateTime? StartProgressDateTime { get; private set; }
    
    public DateTime? FinishedDateTime { get; private set; }
    
    public DateOnly? DueDate { get; private set; }
    
    private Todo(TodoId id, Title title, Description description, UserId ownerId)
    {
        Id = id;
        Title = title;
        Description = description;
        OwnerId = ownerId;
    }

    public void ChangeDueDate(DateOnly dueDate, DateOnly today)
    {
        if (Status != Status.Todo)
        {
            throw new InvalidOperationException("Due date cannot be changed when already started");
        }

        if (dueDate <= today)
        {
            throw new ArgumentException("Due date must be today or in the future");
        }
        
        DueDate = dueDate;
    }
    
    public void StartProgress(DateTime now)
    {
        if (Status != Status.Todo)
        {
            throw new InvalidOperationException("Can only start Progress when todo wasn't started yet");
        }

        Status = Status.InProgress;
        StartProgressDateTime = now;
    }

    public void Finish(DateTime now)
    {
        if (Status != Status.InProgress)
        {
            throw new InvalidOperationException("Cannot finish the todo when it was not started yet");
        }
        
        Status = Status.Finished;
        FinishedDateTime = now;
    }
    
    public static Todo Create(Title title, Description description, UserId ownerId, DateTime now)
    {
        var id = new TodoId(Guid.NewGuid());
        return new Todo(id, title, description, ownerId)
        {
            CreationDateTime = now,
        };
    }
}