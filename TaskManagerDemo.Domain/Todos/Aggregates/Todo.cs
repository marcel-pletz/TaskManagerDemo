using Microsoft.VisualBasic;
using TaskManagerDemo.Domain.Todos.ValueObjects;
using TaskManagerDemo.Domain.Users.Aggregates;

namespace TaskManagerDemo.Domain.Todos.Aggregates;

public sealed class Todo : IAggregateRoot
{
    public TodoId Id { get; init; }
    
    public Title Title { get; set; }
    
    public Description Description { get; set; }
    
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
        ValidateStatusAllowsDueDateChanges();

        ValidateDueDate(dueDate, today);
        
        DueDate = dueDate;
    }

    private void ValidateStatusAllowsDueDateChanges()
    {
        if (Status != Status.Todo)
        {
            throw new InvalidOperationException("Due date cannot be changed when already started");
        }
    }

    public void RemoveDueDate()
    {
        ValidateStatusAllowsDueDateChanges();
        
        DueDate = null;
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

    public void Update(Title title, Description description)
    {
        Title = title;
        Description = description;
    }
    
    public bool IsOwnedBy(User user)
    {
        return OwnerId == user.Id;
    }
    
    public static Todo Create(Title title, Description description, UserId ownerId, DateTime now, DateOnly? dueDate = null)
    {
        var id = new TodoId(Guid.NewGuid());
        
        if(dueDate.HasValue) 
        {
            ValidateDueDate(dueDate.Value, DateOnly.FromDateTime(now));
        }
        var todo = new Todo(id, title, description, ownerId)
        {
            CreationDateTime = now,
            DueDate = dueDate
        };

        return todo;
    }

    private static void ValidateDueDate(DateOnly dueDate, DateOnly today)
    {
        if (dueDate < today)
        {
            throw new ArgumentException("Due date must be today or in the future");
        }
    }
}