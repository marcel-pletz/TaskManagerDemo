using TaskManagerDemo.Domain.Users.ValueObjects;

namespace TaskManagerDemo.Domain.Users.Aggregates;

public sealed class User : AggregateRoot 
{
    public UserId Id { get; private init; }
    
    public UserName Username { get; private init; }

    public Email Email { get; private init; }
    
    public Role Role { get; private init; }
    
    private User(UserId id, UserName username, Email email, Role role)
    {
        Id = id;
        Username = username;
        Email = email;
        Role = role;
    }

    public static User Create(UserName username, Email email, Role role)
    {
        var id = new UserId(Guid.NewGuid());
        return new User(id, username, email, role);
    }
}