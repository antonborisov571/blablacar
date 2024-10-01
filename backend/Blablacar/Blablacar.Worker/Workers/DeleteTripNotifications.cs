using Blablacar.Core.Requests.Trips.DeleteTripNotification;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blablacar.Worker.Workers;

/// <summary>
/// Удаление прошедших уведомлений
/// </summary>
/// <param name="logger">Логгер</param>
/// <param name="mediator">Медиатор</param>
public class DeleteTripNotifications(
    ILogger<DeleteTripNotifications> logger,
    IMediator mediator
    ): IWorker
{
    /// <inheritdoc />
    public async Task RunAsync()
    {
        logger.LogInformation("Удаление уведомлений о поездке");

        await mediator.Send(new DeleteTripNotificationCommand());
    }  
}