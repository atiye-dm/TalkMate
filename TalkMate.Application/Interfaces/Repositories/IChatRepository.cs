using TalkMate.Domain.Entities;

namespace TalkMate.Application.Interfaces.Repositories;

public interface IChatRepository
{
    Task AddMessageAsync(Message message, CancellationToken cancellationToken);

    Task AddResponseAsync(ChatResponse response, CancellationToken cancellationToken);

    Task<IReadOnlyList<Message>> GetChatHistoryAsync(Guid userId, CancellationToken cancellationToken);
}

