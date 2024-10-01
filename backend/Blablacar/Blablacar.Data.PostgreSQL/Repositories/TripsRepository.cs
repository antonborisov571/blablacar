using Blablacar.Contracts.Enums;
using Blablacar.Contracts.Requests.Trips.GetTrips;
using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Blablacar.Data.PostgreSQL.Repositories;

/// <inheritdoc />
public class TripsRepository(IDbContext dbContext) : AbstractTripsRepository(dbContext)
{
    private readonly IDbContext _dbContext = dbContext;

    /// <inheritdoc />
    public override async Task<List<Trip>> GetTripsByDriver(string driverId) =>
        await _dbContext.Trips.Where(x => x.DriverId == driverId).ToListAsync();

    /// <inheritdoc />
    public override async Task<List<Trip>> GetTripsByRequest(GetTripsRequest request)
    {
        return await _dbContext.Trips
            .Include(t => t.Driver)
            .Include(t => t.Passengers)
            .Where(t => t.Where == request.Where
                && t.WhereFrom == request.WhereFrom
                && t.CountPassengers - t.Passengers.Count >= request.CountPassengers
                && t.DateTimeTrip.Date == request.DateTimeTrip.Date)
            .Where(t => (!request.IsSmoking ?? true) 
                        || t.Driver.PreferencesSmoking != SmokingType.HighSmoking)
            .Where(t => (!request.IsAnimal ?? true) 
                        || t.Driver.PreferencesAnimal != AnimalType.HighAnimal)
            .Where(t => (!request.IsMusic ?? true) 
                        || t.Driver.PreferencesMusic != MusicType.HighMusic)
            .ToListAsync();
    }

    public override async Task<Trip?> GetTripsWithDriverPassengers(int tripId)
    {
        return await _dbContext.Trips
            .Include(t => t.Driver)
            .Include(t => t.Passengers)
            .FirstOrDefaultAsync(t => t.Id == tripId);
    }

    public override async Task<List<Trip>> GetTripsUser(string userId)
    {
        return await _dbContext.Trips
            .Include(t => t.Driver)
            .Include(t => t.Passengers)
            .Where(x => x.DriverId == userId || x.Passengers.Any(y => y.Id == userId))
            .Where(x => x.DateTimeTrip > DateTime.Now)
            .ToListAsync();
    }

    public override async Task<int> AddPassenger(int tripId, string userId)
    {
        var trip = await _dbContext.Trips
            .Include(x => x.Passengers)
            .FirstOrDefaultAsync(x => x.Id == tripId);
        
        if (trip == null)
            throw new NotFoundException("Поездка не найдена");

        if (trip.Passengers.Any(x => x.Id == userId))
            throw new BadRequestException("Этот пользователь уже является пассажиром");
        
        var user = await _dbContext.Users.FindAsync(userId);
        if (user == null)
            throw new NotFoundException("Пользователь не найден");
        
        trip.Passengers.Add(user);
        return await _dbContext.SaveChangesAsync();
    }

    public override async Task<int> DeletePassenger(int tripId, string passengerId, string driverId)
    {
        var trip = await _dbContext.Trips
            .Include(x => x.Passengers)
            .FirstOrDefaultAsync(x => x.Id == tripId);
        
        if (trip == null)
            throw new NotFoundException("Поездка не найдена");

        if (trip.DriverId != driverId)
            throw new BadRequestException("Вы не являетесь водителем данной поездки");
        
        var passenger = await _dbContext.Users.FindAsync(passengerId);
        if (passenger == null)
            throw new NotFoundException("Пассажир не найден");

        if (!trip.Passengers.Contains(passenger))
            throw new BadRequestException("Пользователь не является пассажиром");

        trip.Passengers.Remove(passenger);

        return await _dbContext.SaveChangesAsync();
    }
}