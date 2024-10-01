using FluentValidation;

namespace Blablacar.Core.Requests.Auth.PostRefreshToken;

/// <summary>
/// Валидатор для <see cref="PostRefreshTokenCommand"/>
/// </summary>
public class PostRefreshTokenCommandValidator : AbstractValidator<PostRefreshTokenCommand>
{
    /// <inheritdoc />
    public PostRefreshTokenCommandValidator()
    {
        RuleFor(command => command.AccessToken).NotEmpty();
        RuleFor(command => command.RefreshToken).NotEmpty();
    }
}