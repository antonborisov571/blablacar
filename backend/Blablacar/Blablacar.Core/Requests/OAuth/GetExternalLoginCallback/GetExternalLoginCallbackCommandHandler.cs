using System.Security.Claims;
using Blablacar.Contracts.Requests.OAuth.GetExternalLoginCallback;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Blablacar.Core.Abstractions;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Enums;
using Blablacar.Core.Exceptions.AccountExceptions;
using Blablacar.Core.Exceptions.AuthExceptions;
using Blablacar.Core.Exceptions.OAuthAccountExceptions;
using Blablacar.Core.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.OAuth.GetExternalLoginCallback;

/// <summary>
/// Обработчик для <see cref="GetExternalLoginCallbackCommand"/>
/// </summary>
/// <param name="userManager">UserManager{User} из Identity</param>
/// <param name="jwtGenerator">Генератор JWT токенов</param>
/// <param name="signInManager">SignInManager{User} из Identity</param>
/// <param name="logger">Логгер</param>
public class GetExternalLoginCallbackCommandHandler(
    IJwtGenerator jwtGenerator,
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    ILogger<GetExternalLoginCallbackCommandHandler> logger
    ) :
    IRequestHandler<GetExternalLoginCallbackCommand, GetExternalLoginCallbackResponse>
{
    /// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
    public async Task<GetExternalLoginCallbackResponse> Handle(GetExternalLoginCallbackCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(GetExternalLoginCallbackCommand));
        
         if (request is null)
            throw new ArgumentNullException(nameof(request));
         var info = await signInManager.GetExternalLoginInfoAsync();
        
         if (info is null)
             throw new ExternalLoginInfoNotFoundException(AuthErrorMessages.ExternalLoginInfoNotFound);

         var claims = info.Principal.Claims
             .Where(x => !x.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))
             .ToList();

         var email = claims.GetClaimValueOf(ClaimTypes.Email);
         if (email is null)
             throw new EmailClaimNotFoundException(AuthErrorMessages.EmailClaimNotFound);

         var user = await userManager.FindByEmailAsync(email);
        
         if (user is null)
         {
             user = new User
             {
                 UserName = claims.GetClaimValueOf(ClaimTypes.Name) ??
                            $"{claims.GetClaimValueOf(ClaimTypes.GivenName)}" +
                            $" {claims.GetClaimValueOf(ClaimTypes.Surname)}", 
                 FirstName = claims.GetClaimValueOf(ClaimTypes.GivenName) ??
                             $"{claims.GetClaimValueOf(ClaimTypes.Name)}",
                 LastName = claims.GetClaimValueOf(ClaimTypes.Surname) ??
                            $"{claims.GetClaimValueOf(ClaimTypes.GivenName)}",
                 Birthday = DateTime.TryParse(claims.GetClaimValueOf(ClaimTypes.DateOfBirth)!, out DateTime bdate) 
                     ? bdate 
                     : DateTime.UtcNow.Date,
                 DateRegistration = DateTime.UtcNow.Date,
                 Email = email,
                 EmailConfirmed = true
             };

             var createUserResult = await userManager.CreateAsync(user);

             if (!createUserResult.Succeeded)
                 throw new RegisterUserException(string.Join("\n",
                     createUserResult.Errors.Select(error => error.Description)));
            
         }

         claims.Add(new(ClaimTypes.NameIdentifier, user.Id.ToString()));
            
         var jwt = jwtGenerator.GenerateToken(claims);
         var refreshToken = jwtGenerator.GenerateRefreshToken();
        
         user.AccessToken = jwt;
         user.RefreshToken = refreshToken;
         user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(TokenConfiguration.RefreshTokenExpiryDays);

         await userManager.UpdateAsync(user);
         
         logger.LogInformation("Обработка запроса {name} завершена" +
                               "Время: {dateTime}", 
             nameof(GetExternalLoginCallbackCommand), DateTime.Now);
         
         return new GetExternalLoginCallbackResponse
             { AccessToken = jwt, RefreshToken = refreshToken };
    }
}