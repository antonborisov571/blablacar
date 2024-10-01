namespace Blablacar.Core.Entities;

/// <summary>
/// Сущность поездки
/// </summary>
public class Trip : IEntity<int>
{
    /// <summary>
    /// Id поездки
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Навигационное свойство для водителя
    /// </summary>
    public string DriverId { get; set; } = default!;

    /// <summary>
    /// Водитель
    /// </summary>
    public User Driver { get; set; } = default!;

    /// <summary>
    /// Пассажиры
    /// </summary>
    public List<User> Passengers { get; set; } = new();

    /// <summary>
    /// Откуда
    /// </summary>
    public string WhereFrom { get; set; } = default!;

    /// <summary>
    /// Куда
    /// </summary>
    public string Where { get; set; } = default!;
    
    /// <summary>
    /// Дата со временем поездки
    /// </summary>
    public DateTime DateTimeTrip { get; set; }
    
    /// <summary>
    /// Кол-во пассажиров
    /// </summary>
    public int CountPassengers { get; set; }
    
    /// <summary>
    /// Цена поездки
    /// </summary>
    public int Price { get; set; }
}