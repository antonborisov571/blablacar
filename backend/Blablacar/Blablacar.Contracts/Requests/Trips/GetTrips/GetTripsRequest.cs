namespace Blablacar.Contracts.Requests.Trips.GetTrips;

/// <summary>
/// Запрос на поиск поездок
/// </summary>
public class GetTripsRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public GetTripsRequest(GetTripsRequest request)
    {
        WhereFrom = request.WhereFrom;
        Where = request.Where;
        DateTimeTrip = request.DateTimeTrip;
        CountPassengers = request.CountPassengers;
        IsSmoking = request.IsSmoking ?? false;
        IsAnimal = request.IsAnimal ?? false;
        IsMusic = request.IsMusic ?? false;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public GetTripsRequest()
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
    /// Время начала поездки
    /// </summary>
    public DateTime DateTimeTrip { get; set; }
    
    /// <summary>
    /// Кол-во пассажиров
    /// </summary>
    public int CountPassengers { get; set; }
    
    /// <summary>
    /// Можно ли курить
    /// </summary>
    public bool? IsSmoking { get; set; }
    
    /// <summary>
    /// Можно ли животных брать
    /// </summary>
    public bool? IsAnimal { get; set; }
    
    /// <summary>
    /// Можно ли с музыкой
    /// </summary>
    public bool? IsMusic { get; set; }
}