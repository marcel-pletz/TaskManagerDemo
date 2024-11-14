using Microsoft.Extensions.Logging;
using TaskManagerDemo.Domain.Todos.Events;

namespace TaskManagerDemo.Application.DomainEventHandlers;

public class NotifyUserWhenTodoIsNearOverdueAfterDueDateChanged(
    ILogger<NotifyUserWhenTodoIsNearOverdueAfterDueDateChanged> logger,
    TimeProvider timeProvider) 
    : IDomainEventHandler<TodoDueDateChanged>
{
    public Task Handle(TodoDueDateChanged notification, CancellationToken cancellationToken)
    {
        var now = timeProvider.GetUtcNow().DateTime;
        var tomorrow = DateOnly.FromDateTime(now).AddDays(1);

        if (notification.NewDueDate <= tomorrow)
        {
            logger.LogWarning($"todo {notification.Id} is overdue now");
        }
        
        return Task.CompletedTask;
    }
}