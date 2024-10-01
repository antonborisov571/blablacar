using Blablacar.Contracts.Requests.Auth.PostRegister;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Enums;
using Blablacar.Core.Exceptions;
using Blablacar.Core.Exceptions.AuthExceptions;
using Blablacar.Core.Extensions;
using Blablacar.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.Auth.PostRegister;

/// <summary>
/// Обработчик для <see cref="PostRegisterCommand"/>
/// </summary>
/// <param name="userManager">UserManager{User} из Identity</param>
/// <param name="emailSender">Email sender <see cref="IEmailSender"/></param>
/// <param name="logger">Логгер</param>
public class PostRegisterCommandHandler(
    UserManager<User> userManager,
    IEmailSender emailSender,
    ILogger<PostRegisterCommandHandler> logger,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<PostRegisterCommand, PostRegisterResponse>
{

    /// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
    public async Task<PostRegisterResponse> Handle(PostRegisterCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(PostRegisterCommand));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is not null)
            throw new EmailAlreadyRegisteredException(AuthErrorMessages.UserWithSameEmail);

        user = new User
        {
            Email = request.Email, 
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Birthday = request.Birthday,
            DateRegistration = DateTime.Today,
            
            SecurityStamp = Guid.NewGuid().ToString(),
            EmailConfirmed = false
        };
        
        var result = await userManager.CreateAsync(user, request.Password);
        
        if (!result.Succeeded)
            throw new RegisterUserException(
                string.Join("\n", result.Errors.Select(error => error.Description)));
        
        var confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);

        var routeValues = new RouteValueDictionary
        {
            ["code"] = confirmationToken,
            ["email"] = request.Email
        };

        if (routeValues["code"] == null || routeValues["email"] == null)
            throw new BadRequestException("Запрос без почты!");
        
        var messageTemplate =
            await EmailTemplateHelper.GetEmailTemplateAsync(Templates.SendEmailConfirmationMessage,
                cancellationToken);

        if (httpContextAccessor.HttpContext == null)
            throw new BadRequestException("Не было запроса!");
        
        var httpRequest = httpContextAccessor.HttpContext.Request;
        var baseConfirmEmailUrl = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/confirmEmail";
        
        var uriBuilder = new UriBuilder(baseConfirmEmailUrl)
        {
            Query = $"code={Uri.EscapeDataString(routeValues["code"]!.ToString()!)}&email={Uri.EscapeDataString(routeValues["email"].ToString())}"
        };

        var confirmEmailUrl = uriBuilder.ToString();
        
        var placeholders = new Dictionary<string, string> { ["{confirmationToken}"] = confirmEmailUrl };
        
        var message = messageTemplate.ReplacePlaceholders(placeholders);
            
        await emailSender.SendEmailAsync(user.Email,
            message, cancellationToken);
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(PostRegisterCommand), DateTime.Now);
        
        return new PostRegisterResponse { Email = request.Email };
    }
}