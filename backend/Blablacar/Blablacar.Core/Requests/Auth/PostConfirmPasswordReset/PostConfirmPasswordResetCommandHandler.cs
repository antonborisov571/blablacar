using Blablacar.Core.Entities;
using Blablacar.Core.Enums;
using Blablacar.Core.Exceptions.AccountExceptions;
using Blablacar.Core.Exceptions.AuthExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.Auth.PostConfirmPasswordReset;

/// <summary>
/// Обработчик для <see cref="PostConfirmPasswordResetCommand"/>
/// </summary>
/// <param name="userManager">UserManager{User} из Identity</param>
/// <param name="logger">Логгер</param>
public class PostConfirmPasswordResetCommandHandler(
    UserManager<User> userManager,
    ILogger<PostConfirmPasswordResetCommandHandler> logger
    ) : IRequestHandler<PostConfirmPasswordResetCommand>
{
    /// <inheritdoc cref="IRequestHandler{TRequest, TResponse}"/>
    public async Task Handle(PostConfirmPasswordResetCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(PostConfirmPasswordResetCommand));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new NotFoundUserException(AuthErrorMessages.UserNotFound);

        var passwordResetResult = await userManager.ResetPasswordAsync(user, 
            request.VerificationCodeFromUser, request.NewPassword);
        
        if (!passwordResetResult.Succeeded)
            throw new WrongConfirmationTokenException(AuthErrorMessages.WrongConfirmationToken);
        
        user.SecurityStamp = userManager.GenerateNewAuthenticatorKey();
        user.RefreshToken = null;
        
        await userManager.UpdateAsync(user);
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(PostConfirmPasswordResetCommand), DateTime.Now);
    }
}