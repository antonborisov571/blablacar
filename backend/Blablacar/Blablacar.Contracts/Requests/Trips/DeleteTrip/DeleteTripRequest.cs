namespace Blablacar.Contracts.Requests.Trips.DeleteTrip;

/// <summary>
/// Запрос на удаление поездки
/// </summary>
public class DeleteTripRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public DeleteTripRequest(DeleteTripRequest request)
    {
        TripId = request.TripId;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public DeleteTripRequest()
    {
    }
    
    /// <summary>
    /// Id поездки
    /// </summary>
    public int TripId { get; set; }
}