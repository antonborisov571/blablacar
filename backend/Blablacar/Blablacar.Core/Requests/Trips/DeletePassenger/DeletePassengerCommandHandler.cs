using System.Globalization;
using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions;
using Blablacar.Core.Exceptions.AccountExceptions;
using Blablacar.Core.Exceptions.AuthExceptions;
using Blablacar.Core.Extensions;
using Blablacar.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.Trips.DeletePassenger;

/// <summary>
/// Обработчик для <see cref="DeletePassengerCommand"/>
/// </summary>
/// <param name="userContext">UserContext для получения id пользователя</param>
/// <param name="userManager">UserManager из Identity</param>
/// <param name="tripsRepository">Репозиторий поездок</param>
/// <param name="emailSender">EmailSender</param>
/// <param name="logger">Логгер</param>
public class DeletePassengerCommandHandler(
    IUserContext userContext,
    UserManager<User> userManager,
    AbstractTripsRepository tripsRepository,
    IEmailSender emailSender,
    ILogger<DeletePassengerCommandHandler> logger
    ) : IRequestHandler<DeletePassengerCommand>
{
    /// <inheritdoc />
    public async Task Handle(DeletePassengerCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(DeletePassengerCommand));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        
        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("Нет пользователя");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"User with id: {userId}");

        await tripsRepository.DeletePassenger(request.TripId, request.PassengerId, user.Id);

        var trip = (await tripsRepository.GetTripsWithDriverPassengers(request.TripId))!;
        
        var messageTemplate =
            await EmailTemplateHelper.GetEmailTemplateAsync(Templates.SendDeletePassengerNotification,
                cancellationToken);
        
        var placeholders = new Dictionary<string, string>
        {
            ["{whereFrom}"] = trip.WhereFrom,
            ["{where}"] = trip.Where,
            ["{driverName}"] = user.FirstName,
            ["{dateTimeTrip}"] = trip.DateTimeTrip.ToString("f", CultureInfo.CurrentCulture)
        };
        
        var messageMail = messageTemplate.ReplacePlaceholders(placeholders);
        
        await emailSender.SendEmailAsync((await userManager.FindByIdAsync(request.PassengerId))!.Email ?? "",
            messageMail, cancellationToken);
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(DeletePassengerCommand), DateTime.Now);
    }
}