using TaskManagerDemo.Domain.Todos.ValueObjects;

namespace TaskManagerDemo.Domain.Todos.Events;

public sealed record TodoDueDateRemoved(TodoId Id) : DomainEvent;