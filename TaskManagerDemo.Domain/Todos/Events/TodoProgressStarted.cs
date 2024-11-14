using TaskManagerDemo.Domain.Todos.ValueObjects;

namespace TaskManagerDemo.Domain.Todos.Events;

public sealed record TodoProgressStarted(TodoId Id, DateTime StartProgressDateTime) : DomainEvent;