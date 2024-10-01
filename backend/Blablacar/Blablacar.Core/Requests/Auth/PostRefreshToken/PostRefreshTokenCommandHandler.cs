using System.Security.Claims;
using Blablacar.Contracts.Requests.Account.PostRefreshToken;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Enums;
using Blablacar.Core.Exceptions.AccountExceptions;
using Blablacar.Core.Exceptions.AuthExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.Auth.PostRefreshToken;

/// <summary>
/// Обработчик для <see cref="PostRefreshTokenCommand"/>
/// </summary>
/// <param name="userManager">UserManager{User} из Identity</param>
/// <param name="jwtGenerator">Генератор JWT токенов</param>
/// <param name="logger">Логгер</param>
public class PostRefreshTokenCommandHandler(
    UserManager<User> userManager, 
    IJwtGenerator jwtGenerator,
    ILogger<PostRefreshTokenCommandHandler> logger
    ) : IRequestHandler<PostRefreshTokenCommand, PostRefreshTokenResponse>
{
    /// <inheritdoc cref="IRequestHandler{TRequest, TResponse}"/>
    public async Task<PostRefreshTokenResponse> Handle(PostRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(PostRefreshTokenCommand));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var principal = jwtGenerator.GetPrincipalFromExpiredToken(request.AccessToken);

        if (principal is null)
            throw new InvalidTokenException(AuthErrorMessages.InvalidAccessToken);

        var userEmail = principal.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;

        var user = await userManager.FindByEmailAsync(userEmail);

        if (user is null)
            throw new InvalidTokenException(AuthErrorMessages.InvalidAccessToken);
        
        if (user.RefreshToken != request.RefreshToken.Replace(' ', '+')
            || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            throw new InvalidTokenException(AuthErrorMessages.InvalidRefreshToken);

        var newAccessToken = jwtGenerator.GenerateToken(principal.Claims.ToList());
        var newRefreshToken = jwtGenerator.GenerateRefreshToken();

        user.AccessToken = newAccessToken;
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(TokenConfiguration.RefreshTokenExpiryDays);
        
        await userManager.UpdateAsync(user);
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(PostRefreshTokenCommand), DateTime.Now);

        return new PostRefreshTokenResponse { AccessToken = newAccessToken, RefreshToken = newRefreshToken };
    }
}