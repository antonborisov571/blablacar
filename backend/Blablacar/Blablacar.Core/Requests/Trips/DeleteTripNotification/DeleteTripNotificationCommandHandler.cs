using Blablacar.Core.Abstractions.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.Trips.DeleteTripNotification;

/// <summary>
/// Обработчик для <see cref="DeleteTripNotificationCommand"/>
/// </summary>
/// <param name="tripNotificationsRepository">Репозиторий уведомлений о поездках</param>
/// <param name="logger">Логгер</param>
public class DeleteTripNotificationCommandHandler(
    AbstractTripNotificationsRepository tripNotificationsRepository,
    ILogger<DeleteTripNotificationCommandHandler> logger
    ) : IRequestHandler<DeleteTripNotificationCommand>
{
    /// <inheritdoc />
    public async Task Handle(DeleteTripNotificationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(DeleteTripNotificationCommand));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        
        await tripNotificationsRepository.RemoveTripNotifications();
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(DeleteTripNotificationCommand), DateTime.Now);
    }
}