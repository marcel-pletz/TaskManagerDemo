using TaskManagerDemo.Domain.Todos.ValueObjects;

namespace TaskManagerDemo.Domain.Todos.Events;

public sealed record TodoUpdated(TodoId Id) : DomainEvent;