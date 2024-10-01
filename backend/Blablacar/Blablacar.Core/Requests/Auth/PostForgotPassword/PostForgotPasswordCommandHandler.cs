using Blablacar.Core.Abstractions;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Enums;
using Blablacar.Core.Exceptions.AccountExceptions;
using Blablacar.Core.Exceptions.AuthExceptions;
using Blablacar.Core.Extensions;
using Blablacar.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.Auth.PostForgotPassword;


/// <summary>
/// Обработчик для <see cref="PostForgotPasswordCommand"/>
/// </summary>
/// <param name="userManager">UserManager{User} из Identity</param>
/// <param name="emailSender">Email sender <see cref="IEmailSender"/></param>
/// <param name="logger">Логгер</param>
public class PostForgotPasswordCommandHandler(
    UserManager<User> userManager, 
    IEmailSender emailSender,
    ILogger<PostForgotPasswordCommandHandler> logger) : 
    IRequestHandler<PostForgotPasswordCommand>
{
    /// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>>
    public async Task Handle(PostForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(PostForgotPasswordCommand));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        
        var user = await userManager.FindByEmailAsync(request.Email);
        
        if (user is null)
            throw new NotFoundUserException(AuthErrorMessages.UserNotFound);

        if (!await userManager.IsEmailConfirmedAsync(user))
            throw new UnconfirmedEmailException(AuthErrorMessages.NotConfirmedEmail);
        
        var confirmationToken = await userManager.GeneratePasswordResetTokenAsync(user);

        var routeValues = new RouteValueDictionary
        {
            ["code"] = confirmationToken,
            ["email"] = request.Email
        };
        
        var messageTemplate =
            await EmailTemplateHelper.GetEmailTemplateAsync(Templates.SendForgotPasswordMessage,
                cancellationToken);

        var resetPasswordUrl = $"http://localhost:5173/resetPassword?code={routeValues["code"]}&email={routeValues["email"]}";
        
        var placeholders = new Dictionary<string, string> { ["{confirmationToken}"] = resetPasswordUrl };
        
        var message = messageTemplate.ReplacePlaceholders(placeholders);
            
        await emailSender.SendEmailAsync(user.Email!,
            message, cancellationToken);
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(PostForgotPasswordCommand), DateTime.Now);
    }
}