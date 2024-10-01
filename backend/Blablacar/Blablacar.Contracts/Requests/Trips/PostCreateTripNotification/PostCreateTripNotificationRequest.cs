namespace Blablacar.Contracts.Requests.Trips.PostCreateTripNotification;

/// <summary>
/// Запрос для уведомления
/// </summary>
public class PostCreateTripNotificationRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request"><see cref="PostCreateTripNotificationRequest"/></param>
    public PostCreateTripNotificationRequest(PostCreateTripNotificationRequest request)
    {
        WhereFrom = request.WhereFrom;
        Where = request.Where;
        DateTimeTrip = request.DateTimeTrip;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public PostCreateTripNotificationRequest()
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
}