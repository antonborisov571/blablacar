using AutoMapper;
using Blablacar.Contracts.Requests.Users.GetUserInfo;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions.AccountExceptions;
using Blablacar.Core.Exceptions.AuthExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.Users.GetUserInfo;

/// <summary>
/// Обработчик для <see cref="GetUserInfoQuery"/>
/// </summary>
/// <param name="userManager">UserManager из Identity</param>
/// <param name="mapper">Маппер</param>
/// <param name="avatarService">Сервис для работы с аватарками</param>
/// <param name="logger">Логгер</param>
public class GetUserInfoQueryHandler(
    UserManager<User> userManager,
    IMapper mapper,
    IAvatarService avatarService,
    ILogger<GetUserInfoQueryHandler> logger
    )
    : IRequestHandler<GetUserInfoQuery, GetUserInformationResponse>
{
    /// <inheritdoc />
    public async Task<GetUserInformationResponse> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(GetUserInfoQuery));
        
        var user = await userManager.Users
            .Include(u => u.DriverTrips)
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken: cancellationToken);

        if (user is null)
            throw new NotFoundUserException($"Пользователь с id: {request.Id}");

        user.Avatar = await avatarService.GetAvatar(user, cancellationToken);

        var response = mapper.Map<GetUserInformationResponse>(user);
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(GetUserInfoQuery), DateTime.Now);

        return response;
    }
}