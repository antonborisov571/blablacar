namespace Blablacar.Contracts.Requests.Trips.PostCreateTrip;

/// <summary>
/// Запрос для создания поездки
/// </summary>
public class PostCreateTripRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request"><see cref="PostCreateTripRequest"/></param>
    public PostCreateTripRequest(PostCreateTripRequest request)
    {
        WhereFrom = request.WhereFrom;
        Where = request.Where;
        DateTimeTrip = request.DateTimeTrip;
        CountPassengers = request.CountPassengers;
        Price = request.Price;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public PostCreateTripRequest()
    {
    }
    
    /// <summary>
    /// Откуда
    /// </summary>
    public string WhereFrom { get; set; } = default!;
    
    /// <summary>
    /// Куда
    /// </summary>
    public string Where { get; set; } = default!;
    
    /// <summary>
    /// Время поездки
    /// </summary>
    public DateTime DateTimeTrip { get; set; }
    
    /// <summary>
    /// Количество пассажиров
    /// </summary>
    public int CountPassengers { get; set; }
    
    /// <summary>
    /// Цена
    /// </summary>
    public int Price { get; set; }
}