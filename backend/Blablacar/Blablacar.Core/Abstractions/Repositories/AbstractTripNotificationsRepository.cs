using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;

namespace Blablacar.Core.Abstractions.Repositories;

/// <summary>
/// Репозиторий для уведомлений о поездках
/// </summary>
/// <param name="dbContext">Контекст базы данных</param>
public abstract class AbstractTripNotificationsRepository(IDbContext dbContext)
    : GenericRepository<TripNotification, int>(dbContext)
{
    /// <summary>
    /// Получить уведомления
    /// </summary>
    /// <param name="where">Куда</param>
    /// <param name="whereFrom">Откуда</param>
    /// <param name="dateTimeTrip">Дата поездки</param>
    /// <returns>Уведомления</returns>
    public abstract Task<List<TripNotification>> GetTripNotifications(string where, string whereFrom, DateTime dateTimeTrip);
    
    /// <summary>
    /// Удалить уже прошедшие уведомления
    /// </summary>
    /// <returns>Количество изменённых строк</returns>
    public abstract Task<int> RemoveTripNotifications();
}