using Blablacar.Contracts.Requests.Trips.GetTripsUser;
using MediatR;

namespace Blablacar.Core.Requests.Trips.GetTripsUser;

/// <summary>
/// Команда на получение поездок пользователя
/// </summary>
public class GetTripsUserQuery : IRequest<GetTripsUserResponse>
{
    
}