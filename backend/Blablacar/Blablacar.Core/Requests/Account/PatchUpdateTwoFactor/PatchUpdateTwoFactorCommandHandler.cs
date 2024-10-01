using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions;
using Blablacar.Core.Exceptions.AuthExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Blablacar.Core.Requests.Account.PatchUpdateTwoFactor;

/// <summary>
/// Обработчик для смены двухфакторной авторизации
/// </summary>
/// <param name="userContext"></param>
/// <param name="userManager"></param>
public class PatchUpdateTwoFactorCommandHandler(
    IUserContext  userContext,
    UserManager<User> userManager
    ) : IRequestHandler<PatchUpdateTwoFactorCommand>
{
    /// <inheritdoc />
    public async Task Handle(PatchUpdateTwoFactorCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("Нет пользователя");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"User with id: {userId}");

        await userManager.SetTwoFactorEnabledAsync(user, request.TwoFactorEnabled);
    }
}