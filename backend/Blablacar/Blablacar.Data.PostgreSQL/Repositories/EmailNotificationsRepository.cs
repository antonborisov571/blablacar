using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blablacar.Data.PostgreSQL.Repositories;

public class EmailNotificationsRepository(IDbContext dbContext) 
    : AbstractEmailNotificationsRepository(dbContext)
{
    private readonly IDbContext _dbContext = dbContext;

    public override async Task<List<EmailNotification>> GetNotSentNotifications(int takeCount)
    {
        return await _dbContext.EmailNotifications
            .Where(x => x.SentDate == null)
            .Take(takeCount)
            .ToListAsync();
        
    }
}