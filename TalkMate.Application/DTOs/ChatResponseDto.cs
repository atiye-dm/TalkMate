namespace TalkMate.Application.DTOs;

public sealed class ChatResponseDto
{
    public Guid MessageId { get; init; }

    public string ResponseText { get; init; } = string.Empty;

    public DateTime CreatedAt { get; init; }
}

