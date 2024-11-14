namespace TaskManagerDemo.Domain.Todos.ValueObjects;

public record TodoId(Guid Value)
{
    public static TodoId From(string id) => new(Guid.Parse(id));
    
    public override string ToString()
    {
        return Value.ToString();
    }
    
    public static implicit operator Guid(TodoId id) => id.Value;
    
    public static implicit operator string(TodoId id) => id.Value.ToString();
};