namespace TaskManagerDemo.Domain.Todos.ValueObjects;

public record TodoId(Guid Value)
{
    public static TodoId From(string id) => new(Guid.Parse(id));
};