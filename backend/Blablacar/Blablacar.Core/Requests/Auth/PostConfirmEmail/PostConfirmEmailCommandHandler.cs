using Blablacar.Core.Entities;
using Blablacar.Core.Enums;
using Blablacar.Core.Exceptions.AccountExceptions;
using Blablacar.Core.Exceptions.AuthExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.Auth.PostConfirmEmail;

/// <summary>
/// Обработчик для <see cref="PostConfirmEmailCommand"/>
/// </summary>
/// <param name="userManager">UserManager из Identity</param>
/// <param name="logger">Логгер</param>
public class PostConfirmEmailCommandHandler
    (UserManager<User> userManager, ILogger<PostConfirmEmailCommandHandler> logger) :
    IRequestHandler<PostConfirmEmailCommand>
{

    /// <inheritdoc cref="IRequestHandler{TRequest}"/>
    public async Task Handle(PostConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(PostConfirmEmailCommand));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new NotFoundUserException(AuthErrorMessages.UserNotFound);

        var verificationResult =
            await userManager.ConfirmEmailAsync(user, request.Code);

        if (!verificationResult.Succeeded)
            throw new WrongConfirmationTokenException(AuthErrorMessages.WrongConfirmationToken);

        user.EmailConfirmed = true;

        await userManager.UpdateAsync(user);
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(PostConfirmEmailCommand), DateTime.Now);
    }
}