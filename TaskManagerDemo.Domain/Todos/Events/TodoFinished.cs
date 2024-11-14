using TaskManagerDemo.Domain.Todos.ValueObjects;

namespace TaskManagerDemo.Domain.Todos.Events;

public sealed record TodoFinished(TodoId Id, DateTime FinishedDateTime) : DomainEvent;