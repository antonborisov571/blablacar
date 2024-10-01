using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.EmailNotificator.SendEmailNotification;

/// <summary>
/// Обработчик для <see cref="SendEmailNotificationCommand"/>
/// </summary>
/// <param name="emailNotificationsRepository">Репозиторий уведомлений</param>
/// <param name="dateTimeProvider">Провайдер дат</param>
/// <param name="emailSender">Работа с почтой</param>
/// <param name="logger">Логгер</param>
public class SendEmailNotificationCommandHandler(
    AbstractEmailNotificationsRepository emailNotificationsRepository,
    IDateTimeProvider dateTimeProvider,
    IEmailSender emailSender,
    ILogger<SendEmailNotificationCommandHandler> logger)
    : IRequestHandler<SendEmailNotificationCommand>
{
    private const int TakeCount = 50;

    /// <inheritdoc />
    public async Task Handle(SendEmailNotificationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(SendEmailNotificationCommand));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        while (true)
        {
            var emailNotifications = await emailNotificationsRepository.GetNotSentNotifications(TakeCount);

            if (!emailNotifications.Any())
                break;

            foreach (var emailNotification in emailNotifications)
            {
                await emailSender.SendEmailAsync(
                    to: emailNotification.EmailTo,
                    message: emailNotification.Body,
                    subject: emailNotification.Head,
                    cancellationToken: cancellationToken);

                emailNotification.SentDate = dateTimeProvider.CurrentDate;
                await emailNotificationsRepository.Update(emailNotification);
            }
        }
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(SendEmailNotificationCommand), DateTime.Now);
    }
}