namespace TalkMate.Domain.Entities;

public class Message
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Text { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public User? User { get; set; }

    public ChatResponse? Response { get; set; }
}

