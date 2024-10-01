using Blablacar.Contracts.Requests.Trips.GetTrips;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;

namespace Blablacar.Core.Abstractions.Repositories;

/// <summary>
/// Репозиторий для поездок
/// </summary>
/// <param name="dbContext">Контекст базы данных</param>
public abstract class AbstractTripsRepository(IDbContext dbContext)
    : GenericRepository<Trip, int>(dbContext)
{
    /// <summary>
    /// Получить все поездки водителя
    /// </summary>
    /// <param name="driverId">id водителя</param>
    /// <returns>Поездки</returns>
    public abstract Task<List<Trip>> GetTripsByDriver(string driverId);

    /// <summary>
    /// Получить поездки по request-у
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <returns>Определенные поездки</returns>
    public abstract Task<List<Trip>> GetTripsByRequest(GetTripsRequest request);

    /// <summary>
    /// Получить поездку с водителем
    /// </summary>
    /// <param name="tripId">Id поездки</param>
    /// <returns>Поездку</returns>
    public abstract Task<Trip?> GetTripsWithDriverPassengers(int tripId);

    /// <summary>
    /// Получить активные поездки пользователя
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    /// <returns>Активные поездки</returns>
    public abstract Task<List<Trip>> GetTripsUser(string userId);

    
    /// <summary>
    /// Добавление пассажира
    /// </summary>
    /// <param name="tripId">Id поездки</param>
    /// <param name="userId">Пользователь</param>
    /// <returns>Количестов измененных строк</returns>
    public abstract Task<int> AddPassenger(int tripId, string userId);
    
    /// <summary>
    /// Удаление пассажира
    /// </summary>
    /// <param name="tripId">Id поездки</param>
    /// <param name="passengerId">Id пользователя</param>
    /// <param name="driverId">Id водителя</param>
    /// <returns>Количестов измененных строк</returns>
    public abstract Task<int> DeletePassenger(int tripId, string passengerId, string driverId);
}