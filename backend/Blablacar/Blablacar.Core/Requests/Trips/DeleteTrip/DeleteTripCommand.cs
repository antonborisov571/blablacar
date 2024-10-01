using Blablacar.Contracts.Requests.Trips.DeleteTrip;
using MediatR;

namespace Blablacar.Core.Requests.Trips.DeleteTrip;

/// <summary>
/// Команда на удаление поездки
/// </summary>
/// <param name="request">Запрос</param>
public class DeleteTripCommand(DeleteTripRequest request) 
    :DeleteTripRequest(request), IRequest;