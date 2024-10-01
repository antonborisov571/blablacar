using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blablacar.Data.PostgreSQL.Repositories;

/// <inheritdoc />
public class MessagesRepository(IDbContext dbContext) : AbstractMessagesRepository(dbContext)
{
    private readonly IDbContext _dbContext = dbContext;

    /// <inheritdoc />
    public override async Task<List<Message>> GetMessagesByReceiver(string senderId, string receiverId)
    {
        return await _dbContext.Messages
            .Where(x => (x.SenderId == senderId && x.ReceiverId == receiverId)
                || (x.SenderId == receiverId && x.ReceiverId == senderId))
            .ToListAsync();
    }

    /// <inheritdoc />
    public override  List<Message> GetChats(string userId)
    {
        return _dbContext.Messages
            .Include(x => x.Sender)
            .Include(x => x.Receiver)
            .Where(x => x.SenderId == userId || x.ReceiverId == userId)
            .AsEnumerable()
            .DistinctBy(x => x.SenderId)
            .DistinctBy(x => x.ReceiverId)
            .ToList();
    }
}