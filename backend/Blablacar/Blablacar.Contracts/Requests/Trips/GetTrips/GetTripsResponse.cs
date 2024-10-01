namespace Blablacar.Contracts.Requests.Trips.GetTrips;

/// <summary>
/// Ответ на запрос <see cref="GetTripsRequest"/>
/// </summary>
public class GetTripsResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public GetTripsResponse()
        => Trips = new List<GetTripsResponseItem>();

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="trips">Поездки</param>
    public GetTripsResponse(IEnumerable<GetTripsResponseItem> trips)
        => Trips = trips.ToList();
    
    /// <summary>
    /// Поездки
    /// </summary>
    public List<GetTripsResponseItem> Trips { get; set; }
}