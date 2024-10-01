using Blablacar.Core.Enums;
using FluentValidation;

namespace Blablacar.Core.Requests.Auth.PostRevokeToken;

/// <summary>
/// Валидатор для <see cref="PostRevokeTokenCommand"/>
/// </summary>
public class PostRevokeTokenCommandValidator : AbstractValidator<PostRevokeTokenCommand>
{
    /// <inheritdoc />
    public PostRevokeTokenCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Почта"));

        RuleFor(command => command.Email)
            .EmailAddress().WithMessage(AuthErrorMessages.InvalidEmailFormat);
    }
}