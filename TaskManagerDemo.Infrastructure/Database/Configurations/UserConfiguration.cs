using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagerDemo.Domain.Users.Aggregates;
using TaskManagerDemo.Domain.Users.ValueObjects;

namespace TaskManagerDemo.Infrastructure.Database.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));
     
        builder.Ignore(x => x.DomainEvents);
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(value => value.Value, value => new UserId(value));

        builder.Property(x => x.Username)
            .HasConversion(value => value.Value, value => new UserName(value));
        
        builder.Property(x => x.Email)
            .HasConversion(value => value.Value, value => new Email(value));

        builder.Property(x => x.Role)
            .HasConversion<int>();
    }
}