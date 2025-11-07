namespace TalkMate.Application.DTOs;

public sealed class ChatRecordDto
{
    public Guid MessageId { get; init; }

    public string MessageText { get; init; } = string.Empty;

    public DateTime MessageCreatedAt { get; init; }

    public string? ResponseText { get; init; }

    public DateTime? ResponseCreatedAt { get; init; }
}

