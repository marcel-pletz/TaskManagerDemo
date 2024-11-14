using MediatR;
using TaskManagerDemo.Domain;

namespace TaskManagerDemo.Application.DomainEventHandlers;

public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent> where  TEvent : DomainEvent;