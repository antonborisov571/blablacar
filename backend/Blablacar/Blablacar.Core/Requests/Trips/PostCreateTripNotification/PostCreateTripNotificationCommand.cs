using Blablacar.Contracts.Requests.Trips.PostCreateTripNotification;
using MediatR;

namespace Blablacar.Core.Requests.Trips.PostCreateTripNotification;

/// <summary>
/// Команда для уведомления
/// </summary>
/// <param name="request">Запрос</param>
public class PostCreateTripNotificationCommand(PostCreateTripNotificationRequest request)
    : PostCreateTripNotificationRequest(request), IRequest;