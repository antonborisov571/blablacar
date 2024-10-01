using Blablacar.Contracts.Requests.Trips.PostTripBooking;
using MediatR;

namespace Blablacar.Core.Requests.Trips.PostTripBooking;

/// <summary>
/// Команда для бронирования
/// </summary>
/// <param name="request">Запрос</param>
public class PostTripBookingCommand(PostTripBookingRequest request) 
    : PostTripBookingRequest(request), IRequest;