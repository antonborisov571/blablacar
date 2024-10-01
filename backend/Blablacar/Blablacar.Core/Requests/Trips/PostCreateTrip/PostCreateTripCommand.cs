using Blablacar.Contracts.Requests.Trips.PostCreateTrip;
using MediatR;

namespace Blablacar.Core.Requests.Trips.PostCreateTrip;

/// <summary>
/// Команда для <see cref="PostCreateTripRequest"/>
/// </summary>
/// <param name="request">Запрос</param>
public class PostCreateTripCommand(PostCreateTripRequest request)
    : PostCreateTripRequest(request), IRequest;