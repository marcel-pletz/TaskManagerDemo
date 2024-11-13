namespace TaskManagerDemo.Domain.Todos.ValueObjects;

public sealed record Title : TextValue
{
    protected override int MaxLength => 100;
    
    public Title(string title) : base(title)
    {
    }
}