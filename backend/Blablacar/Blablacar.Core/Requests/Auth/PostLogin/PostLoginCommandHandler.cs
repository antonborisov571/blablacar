using MediatR;
using Microsoft.AspNetCore.Identity;
using Blablacar.Core.Entities;
using Blablacar.Core.Enums;
using Blablacar.Core.Extensions;
using Blablacar.Core.Models;
using Blablacar.Contracts.Requests.Auth.PostLogin;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Exceptions.AuthExceptions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.Auth.PostLogin;

/// <summary>
/// Обработчик для <see cref="PostLoginCommand"/>
/// </summary>
/// <param name="userManager">UserManager{User} из Identity</param>
/// <param name="jwtGenerator">Генератор JWT</param>
/// <param name="claimsManager">ClaimsManager <see cref="IUserClaimsManager"/> </param>
/// <param name="emailSender">EmailSender <see cref="IEmailSender"/> </param>
/// <param name="logger">Логгер</param>
public class PostLoginCommandHandler(
    UserManager<User> userManager,
    IJwtGenerator jwtGenerator, 
    IUserClaimsManager claimsManager, 
    IEmailSender emailSender,
    ILogger<PostLoginCommandHandler> logger) : IRequestHandler<PostLoginCommand, PostLoginResponse>
{
    /// <inheritdoc />
    public async Task<PostLoginResponse> Handle(PostLoginCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(PostLoginCommand));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new NotFoundUserException(AuthErrorMessages.UserNotFound);

        if (!user.EmailConfirmed)
        {
            var confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);

            var routeValues = new RouteValueDictionary
            {
                ["code"] = confirmationToken,
                ["email"] = request.Email
            };
        
            var messageTemplate =
                await EmailTemplateHelper.GetEmailTemplateAsync(Templates.SendEmailConfirmationMessage,
                    cancellationToken);

            var confirmEmailUrl = $"http://localhost:5173/confirmEmail?code={routeValues["code"]}&email={routeValues["email"]}";
        
            var placeholders = new Dictionary<string, string> { ["{confirmationToken}"] = confirmEmailUrl };

            var message = messageTemplate.ReplacePlaceholders(placeholders);
            
            await emailSender.SendEmailAsync(user.Email!,
                message, cancellationToken);
            
            throw new NotConfirmedEmailException(AuthErrorMessages.NotConfirmedEmail);
        }

        var isCorrectPassword = await userManager.CheckPasswordAsync(user, request.Password);

        if (!isCorrectPassword)
            throw new WrongPasswordException(AuthErrorMessages.WrongPassword);
        
        if (user.TwoFactorEnabled)
        {
            var confirmationToken = await userManager.GenerateTwoFactorTokenAsync(user, "customtokenprovider");

            var routeValues = new RouteValueDictionary
            {
                ["code"] = confirmationToken,
                ["email"] = request.Email
            };
        
            var messageTemplate =
                await EmailTemplateHelper.GetEmailTemplateAsync(Templates.SendEmailConfirmationMessage,
                    cancellationToken);

            var confirmEmailUrl = $"{routeValues["code"]}";
        
            var placeholders = new Dictionary<string, string> { ["{confirmationToken}"] = confirmEmailUrl };

            var message = messageTemplate.ReplacePlaceholders(placeholders);
            
            await emailSender.SendEmailAsync(user.Email!,
                message, cancellationToken);
        }
        
        var userClaims = await claimsManager.GetUserClaimsAsync(user, cancellationToken);

        user.AccessToken = jwtGenerator.GenerateToken(userClaims);
        user.RefreshToken = jwtGenerator.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(TokenConfiguration.RefreshTokenExpiryDays);

        await userManager.UpdateAsync(user);
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(PostLoginCommand), DateTime.Now);
        
        return new PostLoginResponse
        {
            AccessToken = !user.TwoFactorEnabled ? user.AccessToken : "", 
            RefreshToken = !user.TwoFactorEnabled ? user.RefreshToken : "",
            TwoFactorEnabled = user.TwoFactorEnabled
        };
    }
}