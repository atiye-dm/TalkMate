namespace TalkMate.Domain.Entities;

public class ChatResponse
{
    public Guid Id { get; set; }

    public Guid MessageId { get; set; }

    public string ResponseText { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public Message? Message { get; set; }
}

