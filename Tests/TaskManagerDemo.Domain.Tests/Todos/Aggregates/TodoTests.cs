using TaskManagerDemo.Domain.Todos.ValueObjects;
using TaskManagerDemo.Domain.Users.Aggregates;

namespace TaskManagerDemo.Domain.Todos.Aggregates;

public class TodoTests
{
    [Test]
    public void Create_Will_Initialize_A_Valid_Todo()
    {
        var title = new Title("title");
        var description = new Description("description");
        var ownerId = new UserId(Guid.NewGuid());

        var now = DateTime.Now;
        var todo = Todo.Create(title, description, ownerId, now);
        
        Assert.Multiple(() =>
        {
            Assert.That(todo.Id.Value, Is.Not.EqualTo(Guid.Empty));
            Assert.That(todo.Title, Is.EqualTo(title));
            Assert.That(todo.Description, Is.EqualTo(description));
            Assert.That(todo.OwnerId, Is.EqualTo(ownerId));
            Assert.That(todo.Status, Is.EqualTo(Status.Todo));
            Assert.That(todo.CreationDateTime, Is.EqualTo(now));
            Assert.That(todo.StartProgressDateTime, Is.Null);
            Assert.That(todo.FinishedDateTime, Is.Null);
        });
    }

    [Test]
    public void StartProgress_Will_Set_The_Status_To_InProgress()
    {
        var todo = CreateDefaultTodo();
        
        todo.StartProgress(DateTime.Now);
        
        Assert.That(todo.Status, Is.EqualTo(Status.InProgress));
    }
    
    [Test]
    public void StartProgress_Will_Set_The_StartProgressDateTime_To_Given_Value()
    {
        var todo = CreateDefaultTodo();
        var now = DateTime.Now;
        todo.StartProgress(now);
        
        Assert.That(todo.StartProgressDateTime, Is.EqualTo(now));
    }

    [Test]
    public void StartProgress_Is_Possible_When_The_Previous_Status_Is_Todo()
    {
        var todo = CreateDefaultTodo();
        Assert.That(() => todo.StartProgress(DateTime.Now), Throws.Nothing);
    }
    
    [Test]
    public void StartProgress_Will_Throw_When_Todo_Is_Already_In_Progress()
    {
        var todo = CreateTodoInProgress();
        Assert.That(() => todo.StartProgress(DateTime.Now), Throws.InvalidOperationException);
    }
    
    [Test]
    public void StartProgress_Will_Throw_When_Todo_Is_Already_Finished()
    {
        var todo = CreateFinishedTodo();
        Assert.That(() => todo.StartProgress(DateTime.Now), Throws.InvalidOperationException);
    }

    [Test]
    public void Finish_Will_Set_The_Status_To_Finished()
    {
        var todo = CreateTodoInProgress();
        
        todo.Finish(DateTime.Now);
        
        Assert.That(todo.Status, Is.EqualTo(Status.Finished));
    }
    
    [Test]
    public void Finish_Will_Set_The_FinishedDateTime_To_Given_Value()
    {
        var todo = CreateTodoInProgress();
        var now = DateTime.Now;
        todo.Finish(now);
        
        Assert.That(todo.FinishedDateTime, Is.EqualTo(now));
    }
    
    [Test]
    public void Finish_Will_Throw_When_Not_Started()
    {
        var todo = CreateDefaultTodo();
        Assert.That(() => todo.Finish(DateTime.Now), Throws.InvalidOperationException);
    }
    
    [Test]
    public void Finish_Is_Possible_When_The_Previous_Status_Is_InProgress()
    {
        var todo = CreateTodoInProgress();
        Assert.That(() => todo.Finish(DateTime.Now), Throws.Nothing);
    }

    [Test]
    public void Finish_Will_Throw_When_Already_Finished()
    {
        var todo = CreateFinishedTodo();
        
        Assert.That(() => todo.Finish(DateTime.Now), Throws.InvalidOperationException);
    }

    [Test]
    public void DueDate_Cannot_Be_Changed_Once_Todo_Was_Started()
    {
        var todo = CreateTodoInProgress();
        
        var today = DateOnly.FromDateTime(DateTime.Now);
        var tomorrow = DateOnly.FromDateTime(DateTime.Today).AddDays(1);
        Assert.That(() => todo.ChangeDueDate(tomorrow, today), Throws.InvalidOperationException);
    }

    [Test]
    public void DueDate_Cannot_Be_Changed_Once_Todo_Was_Finished()
    {
        var todo = CreateFinishedTodo();
        
        var today = DateOnly.FromDateTime(DateTime.Now);
        var tomorrow = DateOnly.FromDateTime(DateTime.Today).AddDays(1);
        
        Assert.That(() => todo.ChangeDueDate(tomorrow, today), Throws.InvalidOperationException);
    }
    
    [Test]
    public void Due_Date_Can_Be_Changed_When_In_Future()
    {
        var todo = CreateDefaultTodo();
        var today = DateOnly.FromDateTime(DateTime.Now);
        var tomorrow = DateOnly.FromDateTime(DateTime.Today).AddDays(1);
        
        todo.ChangeDueDate(tomorrow, today);
        
        Assert.That(todo.DueDate, Is.EqualTo(tomorrow));
    }

    [Test]
    public void Due_Date_Cannot_Be_Changed_When_In_Past()
    {
        var todo = CreateDefaultTodo();
        var today = DateOnly.FromDateTime(DateTime.Now);
        var tomorrow = DateOnly.FromDateTime(DateTime.Today).AddDays(1);
        
        Assert.That(() => todo.ChangeDueDate(today, tomorrow), Throws.ArgumentException);
    }
    
    private Todo CreateDefaultTodo()
    {
        var title = new Title("title");
        var description = new Description("description");
        var ownerId = new UserId(Guid.NewGuid());

        return Todo.Create(title, description, ownerId, DateTime.Now);
    }

    private Todo CreateTodoInProgress()
    {
        var todo = CreateDefaultTodo();
        todo.StartProgress(DateTime.Now);

        return todo;
    }
    
    private Todo CreateFinishedTodo()
    {
        var todo = CreateTodoInProgress();
        todo.Finish(DateTime.Now);

        return todo;
    }
}