using Microsoft.EntityFrameworkCore;
using TalkMate.Domain.Entities;

namespace TalkMate.Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Message> Messages => Set<Message>();

    public DbSet<ChatResponse> ChatResponses => Set<ChatResponse>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(user => user.Id);
            entity.Property(user => user.Name)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(user => user.CreatedAt)
                .IsRequired();
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(message => message.Id);
            entity.Property(message => message.Text)
                .IsRequired()
                .HasMaxLength(1000);
            entity.Property(message => message.CreatedAt)
                .IsRequired();

            entity.HasOne(message => message.User)
                .WithMany(user => user.Messages)
                .HasForeignKey(message => message.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ChatResponse>(entity =>
        {
            entity.HasKey(response => response.Id);
            entity.Property(response => response.ResponseText)
                .IsRequired()
                .HasMaxLength(1000);
            entity.Property(response => response.CreatedAt)
                .IsRequired();

            entity.HasOne(response => response.Message)
                .WithOne(message => message.Response)
                .HasForeignKey<ChatResponse>(response => response.MessageId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        SeedInitialData(modelBuilder);
    }

    private static void SeedInitialData(ModelBuilder modelBuilder)
    {
        var demoUserId = Guid.Parse("d0fe9715-2043-4d9d-9949-a7e485ace584");
        var demoMessageId = Guid.Parse("2227d6a7-e0fe-45ee-8a36-e6c0b42bfe97");
        var demoResponseId = Guid.Parse("6c95a607-5c8d-4391-8b64-4f9a2e70dd3a");

        var userCreatedAt = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        var messageCreatedAt = userCreatedAt.AddMinutes(5);
        var responseCreatedAt = messageCreatedAt.AddSeconds(10);

        modelBuilder.Entity<User>().HasData(new User
        {
            Id = demoUserId,
            Name = "Demo User",
            CreatedAt = userCreatedAt
        });

        modelBuilder.Entity<Message>().HasData(new Message
        {
            Id = demoMessageId,
            UserId = demoUserId,
            Text = "Feeling a bit stressed about work lately.",
            CreatedAt = messageCreatedAt
        });

        modelBuilder.Entity<ChatResponse>().HasData(new ChatResponse
        {
            Id = demoResponseId,
            MessageId = demoMessageId,
            ResponseText = "It seems you're stressed. Take a deep breath 🌿",
            CreatedAt = responseCreatedAt
        });
    }
}

