using Blablacar.Contracts.Requests.Trips.GetTrip;
using MediatR;

namespace Blablacar.Core.Requests.Trips.GetTrip;

/// <summary>
/// Команда для получения поездок
/// </summary>
public class GetTripQuery : IRequest<GetTripResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="tripId">Id поездки</param>
    public GetTripQuery(int tripId)
    {
        TripId = tripId;
    }
    
    /// <summary>
    /// Id поездки
    /// </summary>
    public int TripId { get; set; }
}