using Blablacar.Core.Requests.EmailNotificator.SendEmailNotification;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blablacar.Worker.Workers;

/// <inheritdoc />
public class EmailNotificator : IWorker
{
    private readonly ILogger<EmailNotificator> _logger;
    private readonly IMediator _mediator;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="logger">Логгер</param>
    /// <param name="mediator">Медиатор CQRS</param>
    public EmailNotificator(ILogger<EmailNotificator> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <inheritdoc />
    public async Task RunAsync()
    {
        _logger.LogInformation("Running email notificator");

        await _mediator.Send(new SendEmailNotificationCommand());
    }
}