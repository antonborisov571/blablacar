using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blablacar.Data.PostgreSQL.Repositories;

public class TripNotificationsRepository(IDbContext dbContext) : AbstractTripNotificationsRepository(dbContext)
{
    private readonly IDbContext _dbContext = dbContext;

    public override async Task<List<TripNotification>> GetTripNotifications(
        string where, 
        string whereFrom, 
        DateTime dateTimeTrip)
    {
        return await _dbContext.TripNotifications
            .Where(x => x.Where == where
                        && x.WhereFrom == whereFrom
                        && x.DateTimeTrip.Date == dateTimeTrip.Date)
            .ToListAsync();
    }

    public override async Task<int> RemoveTripNotifications()
    {
        var tripNotifications = _dbContext.TripNotifications
            .Where(x => x.DateTimeTrip.Date < DateTime.Now);
        _dbContext.TripNotifications.RemoveRange(tripNotifications);

        return await _dbContext.SaveChangesAsync();
    }
}