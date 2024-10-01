using AutoMapper;
using Blablacar.Contracts.Requests.Trips.GetTripsUser;
using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions;
using Blablacar.Core.Exceptions.AccountExceptions;
using Blablacar.Core.Exceptions.AuthExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.Trips.GetTripsUser;

/// <summary>
/// Обработчик для <see cref="GetTripsUserQuery"/>
/// </summary>
/// <param name="userContext">UserContext для получения id пользователя</param>
/// <param name="userManager">UserManager из Identity</param>
/// <param name="tripsRepository">Репозиторий поездок</param>
/// <param name="avatarService">Сервис для работы с аватарками</param>
/// <param name="mapper">Маппер</param>
/// <param name="logger">Логгер</param>
public class GetTripsUserQueryHandler(
    IUserContext userContext,
    UserManager<User> userManager,
    AbstractTripsRepository tripsRepository,
    IAvatarService avatarService,
    IMapper mapper,
    ILogger<GetTripsUserQueryHandler> logger
    ) : IRequestHandler<GetTripsUserQuery, GetTripsUserResponse>
{
    /// <inheritdoc />
    public async Task<GetTripsUserResponse> Handle(GetTripsUserQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(GetTripsUserQuery));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("Нет пользователя");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"User with id: {userId}");

        var trips = await tripsRepository.GetTripsUser(user.Id);
        
        foreach (var driver in trips.Select(t => t.Driver).DistinctBy(d => d.Id))
        {
            driver.Avatar = await avatarService.GetAvatar(driver, cancellationToken);
        }
        
        var responseItems = mapper.Map<List<GetTripsUserResponseItem>>(trips);
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(GetTripsUserQuery), DateTime.Now);

        return new GetTripsUserResponse(responseItems);
    }
}