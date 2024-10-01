namespace Blablacar.Contracts.Requests.Trips.DeletePassenger;

/// <summary>
/// Запрос на удаление пассажира
/// </summary>
public class DeletePassengerRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public DeletePassengerRequest(DeletePassengerRequest request)
    {
        PassengerId = request.PassengerId;
        TripId = request.TripId;
    }

    /// <summary>
    /// Констуктор
    /// </summary>
    public DeletePassengerRequest()
    {
    }
    
    /// <summary>
    /// Id удаляемого пассажира
    /// </summary>
    public string PassengerId { get; set; } = default!;
    
    /// <summary>
    /// Id поездки
    /// </summary>
    public int TripId { get; set; }
}