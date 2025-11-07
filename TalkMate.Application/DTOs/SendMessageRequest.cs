namespace TalkMate.Application.DTOs;

public sealed class SendMessageRequest
{
    public Guid UserId { get; init; }

    public string MessageText { get; init; } = string.Empty;
}

