using Blablacar.Contracts.Enums;

namespace Blablacar.Contracts.Requests.Trips.GetTrip;

/// <summary>
/// Ответ на получение поездки
/// </summary>
public class GetTripResponse
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
    /// Водитель
    /// </summary>
    public Driver Driver { get; set; } = default!;

    /// <summary>
    /// Пассажиры
    /// </summary>
    public List<Passenger> Passengers { get; set; } = new();
    
    /// <summary>
    /// Пользователь пассажир или нет
    /// </summary>
    public bool IsPassenger { get; set; }  
    
    /// <summary>
    /// Пользователь водитель или нет
    /// </summary>
    public bool IsDriver { get; set; }  
    
    /// <summary>
    /// Активная ли поездка
    /// </summary>
    public bool IsActive { get; set; }
}