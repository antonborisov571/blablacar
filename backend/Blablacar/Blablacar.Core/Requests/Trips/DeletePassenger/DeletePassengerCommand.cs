using Blablacar.Contracts.Requests.Trips.DeletePassenger;
using MediatR;

namespace Blablacar.Core.Requests.Trips.DeletePassenger;

/// <summary>
/// Команда на удаление пассажира
/// </summary>
/// <param name="request">Запрос</param>
public class DeletePassengerCommand(DeletePassengerRequest request) 
    : DeletePassengerRequest(request), IRequest;