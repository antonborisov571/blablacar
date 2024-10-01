namespace Blablacar.Contracts.Requests.Trips.PostTripBooking;

/// <summary>
/// Запрос на бронирование поездки
/// </summary>
public class PostTripBookingRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public PostTripBookingRequest(PostTripBookingRequest request)
    {
        TripId = request.TripId;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public PostTripBookingRequest()
    {
    }
    
    /// <summary>
    /// Id поездки
    /// </summary>
    public int TripId { get; set; }
}