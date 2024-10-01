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

namespace Blablacar.Core.Requests.Trips.DeleteTrip;

/// <summary>
/// Обработчик для <see cref="DeleteTripCommand"/>
/// </summary>
/// <param name="userContext">UserContext для получения id пользователя</param>
/// <param name="userManager">UserManager из Identity</param>
/// <param name="tripsRepository">Репозиторий поездок</param>
/// <param name="emailSender">Сервис для отправки email-сообщений</param>
/// <param name="logger">Логгер</param>
public class DeleteTripCommandHandler(
    IUserContext userContext,
    UserManager<User> userManager,
    AbstractTripsRepository tripsRepository,
    IEmailSender emailSender,
    ILogger<DeleteTripCommandHandler> logger
    ) : IRequestHandler<DeleteTripCommand>
{
    /// <inheritdoc />
    public async Task Handle(DeleteTripCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(DeleteTripCommand));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        
        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("Нет пользователя");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"User with id: {userId}");

        var trip = await tripsRepository.GetTripsWithDriverPassengers(request.TripId);

        if (trip == null)
            throw new NotFoundException("Поездка не найдена");

        if (trip.DriverId != user.Id)
            throw new BadRequestException("Вы не являетесь водителем данной поездки");

        foreach (var passenger in trip.Passengers)
        {
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
        
            await emailSender.SendEmailAsync(passenger.Email ?? "",
                messageMail, cancellationToken);
        }

        await tripsRepository.Remove(trip);
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(DeleteTripCommand), DateTime.Now);
    }
}