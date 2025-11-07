using Microsoft.EntityFrameworkCore;
using TalkMate.Application.Interfaces.Repositories;
using TalkMate.Domain.Entities;
using TalkMate.Infrastructure.Data;

namespace TalkMate.Infrastructure.Repositories;

public sealed class ChatRepository : IChatRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ChatRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddMessageAsync(Message message, CancellationToken cancellationToken)
    {
        await _dbContext.Messages.AddAsync(message, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddResponseAsync(ChatResponse response, CancellationToken cancellationToken)
    {
        await _dbContext.ChatResponses.AddAsync(response, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Message>> GetChatHistoryAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Messages
            .AsNoTracking()
            .Include(message => message.Response)
            .Where(message => message.UserId == userId)
            .OrderBy(message => message.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}

