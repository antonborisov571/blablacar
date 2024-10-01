using Blablacar.Contracts.Requests.Account.PostRefreshToken;
using Blablacar.Contracts.Requests.Auth.PostRefreshToken;
using MediatR;

namespace Blablacar.Core.Requests.Auth.PostRefreshToken;

/// <summary>
/// Команда для обновления JWT токена
/// </summary>
public class PostRefreshTokenCommand : PostRefreshTokenRequest, IRequest<PostRefreshTokenResponse>
{
    /// <inheritdoc />
    public PostRefreshTokenCommand(PostRefreshTokenRequest request) : base(request)
    {
    }

    /// <inheritdoc />
    public PostRefreshTokenCommand()
    {
    }
}