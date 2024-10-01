using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions;
using Blablacar.Core.Exceptions.AccountExceptions;
using Blablacar.Core.Exceptions.AuthExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.Trips.PostCreateTripNotification;


/// <summary>
/// Обработчик для <see cref="PostCreateTripNotificationCommand"/>
/// </summary>
/// <param name="userManager">UserManager</param>
/// <param name="userContext">UserContext</param>
/// <param name="tripNotificationsRepository">Репозиторий уведомлемний о поездках</param>
/// <param name="logger">Логгер</param>
public class PostCreateTripNotificationCommandHandler(
    UserManager<User> userManager, 
    IUserContext userContext,
    AbstractTripNotificationsRepository tripNotificationsRepository,
    ILogger<PostCreateTripNotificationCommandHandler> logger
    )
    : IRequestHandler<PostCreateTripNotificationCommand>
{
    /// <inheritdoc />
    public async Task Handle(PostCreateTripNotificationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(PostCreateTripNotificationCommand));
        
        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("User Id was not found");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"User with id: {userId}");

        var tripNotification = new TripNotification
        {
            Where = request.Where,
            WhereFrom = request.WhereFrom,
            DateTimeTrip = request.DateTimeTrip,
            UserId = user.Id
        };

        await tripNotificationsRepository.AddAsync(tripNotification);
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(PostCreateTripNotificationCommand), DateTime.Now);
    }
}