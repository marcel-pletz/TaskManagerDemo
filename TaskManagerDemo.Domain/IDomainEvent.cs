using MediatR;

namespace TaskManagerDemo.Domain;

public abstract record DomainEvent : INotification;