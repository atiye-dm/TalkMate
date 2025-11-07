using TalkMate.Application.DTOs;

namespace TalkMate.Application.Interfaces.Services;

public interface IChatService
{
    Task<ChatResponseDto> SendMessageAsync(SendMessageRequest request, CancellationToken cancellationToken);

    Task<IReadOnlyList<ChatRecordDto>> GetChatHistoryAsync(Guid userId, CancellationToken cancellationToken);
}

