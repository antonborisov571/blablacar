using Blablacar.Contracts.Requests.Auth.PostResetPassword;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Enums;
using Blablacar.Core.Exceptions.AuthExceptions;
using Blablacar.Core.Extensions;
using Blablacar.Core.Models;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.Auth.PostResetPassword;

/// <summary>
/// Обработчик для <see cref="PostResetPasswordCommand"/>
/// </summary>
/// <param name="userManager">UserManager из Identity</param>
/// <param name="emailSender">EmailSender сервис</param>
/// <param name="logger">Логгер</param>
public class PostResetPasswordCommandHandler(
    UserManager<User> userManager, 
    IEmailSender emailSender,
    ILogger<PostResetPasswordCommandHandler> logger) :
    IRequestHandler<PostResetPasswordCommand, PostResetPasswordResponse>
{
    /// <inheritdoc />
    public async Task<PostResetPasswordResponse> Handle(PostResetPasswordCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(PostResetPasswordCommand));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new NotFoundUserException(AuthErrorMessages.UserNotFound);
        
        var isEqualsOldAndNewPasswords = await userManager.CheckPasswordAsync(user, request.NewPassword);

        if (isEqualsOldAndNewPasswords)
            throw new EqualsOldAndNewPasswordsException(AuthErrorMessages.EqualsOldAndNewPasswords);

        var confirmationToken =  await userManager.GeneratePasswordResetTokenAsync(user);

        var messageTemplate =
            await EmailTemplateHelper.GetEmailTemplateAsync(Templates.SendPasswordResetConfirmationMessage,
                cancellationToken);

        var placeholders = new Dictionary<string, string> { ["{confirmationToken}"] = confirmationToken };

        var message = messageTemplate.ReplacePlaceholders(placeholders);
        
        await emailSender.SendEmailAsync(request.Email, message, cancellationToken);
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(PostResetPasswordCommand), DateTime.Now);

        return new PostResetPasswordResponse { Email = request.Email, NewPassword = request.NewPassword };
    }
}