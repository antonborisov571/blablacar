using Blablacar.Core.Entities;
using Blablacar.Core.Enums;
using Blablacar.Core.Exceptions.AccountExceptions;
using Blablacar.Core.Exceptions.AuthExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.Auth.PostRevokeToken;

/// <summary>
/// Обработчик для <see cref="PostRevokeTokenCommand"/>
/// </summary>
/// <param name="userManager">UserManager из Identity</param>
/// <param name="logger">Логгер</param>
public class PostRevokeTokenCommandHandler(
    UserManager<User> userManager,
    ILogger<PostRevokeTokenCommandHandler> logger
    ) : IRequestHandler<PostRevokeTokenCommand>
{
    /// <inheritdoc />
    public async Task Handle(PostRevokeTokenCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(PostRevokeTokenCommand));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new NotFoundUserException(AuthErrorMessages.UserNotFound);

        user.RefreshToken = null;
        await userManager.UpdateAsync(user);
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(PostRevokeTokenCommand), DateTime.Now);
    }
}