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

namespace Blablacar.Core.Requests.Trips.PostTripBooking;


/// <summary>
/// Обработчик для <see cref="PostTripBookingCommand"/>
/// </summary>
/// <param name="userContext">UserContext для получения id пользователя</param>
/// <param name="userManager">UserManager из Identity</param>
/// <param name="tripsRepository">Репозиторий поездок</param>
/// <param name="emailSender">EmailSender</param>
/// <param name="logger">Логгер</param>
public class PostTripBookingCommandHandler(
    IUserContext userContext,
    UserManager<User> userManager,
    AbstractTripsRepository tripsRepository,
    IEmailSender emailSender,
    ILogger<PostTripBookingCommandHandler> logger
    ) : IRequestHandler<PostTripBookingCommand>
{
    /// <inheritdoc />
    public async Task Handle(PostTripBookingCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(PostTripBookingCommand));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("Нет пользователя");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"User with id: {userId}");

        await tripsRepository.AddPassenger(request.TripId, user.Id);

        var trip = await tripsRepository.GetTripsWithDriverPassengers(request.TripId);
        
        var messageTemplate =
            await EmailTemplateHelper.GetEmailTemplateAsync(Templates.SendBookingNotification,
                cancellationToken);
        
        var placeholders = new Dictionary<string, string>
        {
            ["{passengerName}"] = user.FirstName
        };
        
        var messageMail = messageTemplate.ReplacePlaceholders(placeholders);
        
        await emailSender.SendEmailAsync(trip!.Driver.Email ?? "",
            messageMail, cancellationToken);
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(PostTripBookingCommand), DateTime.Now);
    }
}