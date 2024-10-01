using System.Globalization;
using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions;
using Blablacar.Core.Exceptions.AccountExceptions;
using Blablacar.Core.Exceptions.AuthExceptions;
using Blablacar.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.Trips.PostCreateTrip;


/// <summary>
/// Обработчик для <see cref="PostCreateTripCommand"/>
/// </summary>
/// <param name="userManager">UserManager из Identity</param>
/// <param name="userContext">UserContext из Identity</param>
/// <param name="tripsRepository">Репозиторий поездок</param>
/// <param name="tripNotificationsRepository">Репозиторий уведомлений поездок</param>
/// <param name="emailNotificationsRepository">Репозиторий email уведомлений</param>
/// <param name="logger">Логгер</param>
public class PostCreateTripCommandHandler(
    UserManager<User> userManager, 
    IUserContext userContext,
    AbstractTripsRepository tripsRepository,
    AbstractTripNotificationsRepository tripNotificationsRepository,
    AbstractEmailNotificationsRepository emailNotificationsRepository,
    ILogger<PostCreateTripCommandHandler> logger
    ) : IRequestHandler<PostCreateTripCommand>
{
    /// <inheritdoc />
    public async Task Handle(PostCreateTripCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(PostCreateTripCommand));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("User Id was not found");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"User with id: {userId}");

        var tripNotifications = await tripNotificationsRepository.GetTripNotifications(
            request.Where,
            request.WhereFrom,
            request.DateTimeTrip);

        var emailNotificationsTasks = tripNotifications.Select(async t =>
        {
            var emailNotifications1 = await EmailTemplateHelper
                .GetEmailNotificationAsync(
                    placeholders: new Dictionary<string, string>
                    {
                        ["{whereFrom}"] = t.WhereFrom,
                        ["{where}"] = t.Where,
                        ["{dateTimeTrip}"] = t.DateTimeTrip.ToString(CultureInfo.InvariantCulture)
                    },
                    template: Templates.SendTripNotification,
                    head: "Новая поездка",
                    emailTo: (await userManager.FindByIdAsync(t.UserId))?.Email ?? "",
                    cancellationToken: cancellationToken
                );
            return emailNotifications1;
        });
        
        var emailNotifications = await Task.WhenAll(emailNotificationsTasks);

        await emailNotificationsRepository.AddRangeAsync(emailNotifications.ToList());

        await tripsRepository.AddAsync(new Trip
        {
            DriverId = user.Id,
            WhereFrom = request.WhereFrom,
            Where = request.Where,
            DateTimeTrip = request.DateTimeTrip,
            CountPassengers = request.CountPassengers,
            Price = request.Price
        });
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(PostCreateTripCommand), DateTime.Now);
    }
}