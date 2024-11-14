using TaskManagerDemo.Domain.Users.Aggregates;

namespace TaskManagerDemo.Application.Providers;

public interface IUserProvider
{
     Task<User> ProvideCurrentUser(CancellationToken cancellationToken);
}