using System.Security;
using Blablacar.Contracts.Requests.Auth.PostTwoFactor;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Enums;
using Blablacar.Core.Exceptions.AuthExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Blablacar.Core.Requests.Auth.PostTwoFactor;

/// <summary>
/// Обработчик для <see cref="PostTwoFactorCommand"/>
/// </summary>
/// <param name="userManager"></param>
/// <param name="jwtGenerator"></param>
/// <param name="claimsManager"></param>
public class PostTwoFactorCommandHandler(
    UserManager<User> userManager,
    IJwtGenerator jwtGenerator, 
    IUserClaimsManager claimsManager
    ) : IRequestHandler<PostTwoFactorCommand, PostTwoFactorResponse>
{
    /// <inheritdoc />
    public async Task<PostTwoFactorResponse> Handle(PostTwoFactorCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new NotFoundUserException(AuthErrorMessages.UserNotFound);

        if (!await userManager.VerifyTwoFactorTokenAsync(user, "customtokenprovider", request.Code))
            throw new VerificationException();
        
        var userClaims = await claimsManager.GetUserClaimsAsync(user, cancellationToken);

        user.AccessToken = jwtGenerator.GenerateToken(userClaims);
        user.RefreshToken = jwtGenerator.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(TokenConfiguration.RefreshTokenExpiryDays);

        await userManager.UpdateAsync(user);
        
        return new PostTwoFactorResponse
        {
            AccessToken = user.AccessToken, 
            RefreshToken = user.RefreshToken
        };
    }
}