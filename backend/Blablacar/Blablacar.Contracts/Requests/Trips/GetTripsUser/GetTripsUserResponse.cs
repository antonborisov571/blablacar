namespace Blablacar.Contracts.Requests.Trips.GetTripsUser;

/// <summary>
/// Ответ на запрос о получении поездок
/// </summary>
public class GetTripsUserResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public GetTripsUserResponse()
        => Trips = new List<GetTripsUserResponseItem>();

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="trips">Поездки</param>
    public GetTripsUserResponse(IEnumerable<GetTripsUserResponseItem> trips)
        => Trips = trips.ToList();
    
    /// <summary>
    /// Поездки
    /// </summary>
    public List<GetTripsUserResponseItem> Trips { get; set; }
}