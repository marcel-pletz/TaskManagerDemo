using TaskManagerDemo.Domain.Todos.ValueObjects;

namespace TaskManagerDemo.Domain.Todos.Events;

public sealed record TodoDueDateChanged(TodoId Id, DateOnly NewDueDate) : DomainEvent;