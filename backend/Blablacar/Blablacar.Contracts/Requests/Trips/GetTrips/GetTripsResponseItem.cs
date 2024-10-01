namespace Blablacar.Contracts.Requests.Trips.GetTrips;

/// <summary>
/// Поездка
/// </summary>
public class GetTripsResponseItem
{
    /// <summary>
    /// Id поездки
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Откуда
    /// </summary>
    public string WhereFrom { get; set; } = default!;
    
    /// <summary>
    /// Куда
    /// </summary>
    public string Where { get; set; } = default!;
    
    /// <summary>
    /// Время начала поездки
    /// </summary>
    public DateTime DateTimeTrip { get; set; }
    
    /// <summary>
    /// Цена
    /// </summary>
    public int Price { get; set; }
    
    /// <summary>
    /// Имя водителя
    /// </summary>
    public string DriverName { get; set; } = default!;
    
    /// <summary>
    /// Аватар водителя
    /// </summary>
    public string? DriverAvatar { get; set; }
}