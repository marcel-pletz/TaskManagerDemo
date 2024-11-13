using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagerDemo.Domain.Users.Aggregates;
using TaskManagerDemo.Domain.Users.ValueObjects;

namespace TaskManagerDemo.Infrastructure.Database.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));
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
        
        SeedDummyData(builder);
    }

    private void SeedDummyData(EntityTypeBuilder<User> builder)
    {
        var user1 = User.Create(
            new UserName("user1"), 
            new Email("user1@example.com"), 
            Role.User);
        
        var user2 = User.Create(
            new UserName("user2"), 
            new Email("user2@example.com"), 
            Role.User);
        
        var admin = User.Create(
            new UserName("admin"), 
            new Email("admin@example.com"), 
            Role.Administrator);

        builder.HasData(user1, user2, admin);
    }
}