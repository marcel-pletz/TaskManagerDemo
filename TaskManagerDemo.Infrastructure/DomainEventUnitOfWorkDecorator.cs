using MediatR;
using TaskManagerDemo.Domain;
using TaskManagerDemo.Domain.Todos.Repositories;
using TaskManagerDemo.Domain.Users.Repositories;
using TaskManagerDemo.Infrastructure.Database;
using static Microsoft.EntityFrameworkCore.EntityState;

namespace TaskManagerDemo.Infrastructure;

public class DomainEventUnitOfWorkDecorator(IUnitOfWork decorated, TaskManagerDbContext context, IMediator mediator) : IUnitOfWork
{
    public IUserRepository UserRepository => decorated.UserRepository;
    public ITodoRepository TodoRepository => decorated.TodoRepository;
    
    public async Task SaveChanges(CancellationToken cancellationToken)
    {
        await decorated.SaveChanges(cancellationToken);
        
        await PublishAllDomainEvents(cancellationToken);
    }

    private async Task PublishAllDomainEvents(CancellationToken cancellationToken)
    {
        var domainEvents = context.ChangeTracker.Entries()
            .Where(x => x.State is Added or Modified or Deleted)
            .Select(x => x.Entity)
            .Where(x => x is IAggregateRoot)
            .Cast<IAggregateRoot>()
            .SelectMany(x => x.DomainEvents);

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent, cancellationToken);
        }
    }
}