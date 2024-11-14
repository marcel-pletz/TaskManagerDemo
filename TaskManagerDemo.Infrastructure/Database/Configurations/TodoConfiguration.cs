using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagerDemo.Domain.Todos.Aggregates;
using TaskManagerDemo.Domain.Todos.ValueObjects;
using TaskManagerDemo.Domain.Users.Aggregates;

namespace TaskManagerDemo.Infrastructure.Database.Configurations;

public class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToTable(nameof(Todo));

        builder.Ignore(x => x.DomainEvents);
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(value => value.Value, value => new TodoId(value));
        
        builder.Property(x => x.Title)
            .HasConversion(value => value.Value, value => new Title(value))
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasConversion(value => value.Value, value => new Description(value))
            .IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.OwnerId)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.CreationDateTime)
            .HasConversion<DateTime>()
            .IsRequired();
        
        builder.Property(x => x.StartProgressDateTime)
            .HasConversion<DateTime>();

        builder.Property(x => x.FinishedDateTime)
            .HasConversion<DateTime>();
        
        builder.Property(x => x.DueDate)
            .HasConversion<DateOnly>();
    }
}