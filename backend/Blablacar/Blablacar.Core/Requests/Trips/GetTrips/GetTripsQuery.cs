using Blablacar.Contracts.Requests.Trips.GetTrips;
using MediatR;

namespace Blablacar.Core.Requests.Trips.GetTrips;

/// <summary>
/// Команда для запроса о получении поездок
/// </summary>
/// <param name="request">Запрос</param>
public class GetTripsQuery(GetTripsRequest request) 
    : GetTripsRequest(request), IRequest<GetTripsResponse>;