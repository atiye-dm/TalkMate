using Microsoft.EntityFrameworkCore;
using TalkMate.Application.Interfaces.Repositories;
using TalkMate.Domain.Entities;
using TalkMate.Infrastructure.Data;

namespace TalkMate.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);
    }
}

