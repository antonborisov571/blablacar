namespace Blablacar.Core.Entities;

/// <summary>
/// Уведомления о поездках
/// </summary>
public class TripNotification : IEntity<int>
{
    /// <summary>
    /// Id уведомления
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Куда
    /// </summary>
    public string Where { get; set; } = default!;
    
    /// <summary>
    /// Откуда
    /// </summary>
    public string WhereFrom { get; set; } = default!;

    /// <summary>
    /// Дата поездки
    /// </summary>
    public DateTime DateTimeTrip { get; set; }
    
    /// <summary>
    /// Навигационное свойство
    /// </summary>
    public string UserId { get; set; } = default!;
    
    /// <summary>
    /// Для кого уведомление
    /// </summary>
    public User User { get; set; } = default!;
}