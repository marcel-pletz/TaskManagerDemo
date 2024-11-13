namespace TaskManagerDemo.Domain.Todos.ValueObjects;

public sealed record Description : TextValue
{
    protected override int MaxLength => 1000;
    
    public Description(string title) : base(title)
    {
    }
}