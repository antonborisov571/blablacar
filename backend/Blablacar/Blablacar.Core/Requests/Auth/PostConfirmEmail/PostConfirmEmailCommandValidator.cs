using Blablacar.Core.Enums;
using FluentValidation;

namespace Blablacar.Core.Requests.Auth.PostConfirmEmail;

/// <summary>
/// Валидатор для <see cref="PostConfirmEmailCommand"/>
/// </summary>
public class PostConfirmEmailCommandValidator :
    AbstractValidator<PostConfirmEmailCommand>
{
    /// <inheritdoc />
    public PostConfirmEmailCommandValidator()
    {
        RuleFor(command => command)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Почта"));

        RuleFor(command => command.Email)
            .EmailAddress().WithMessage(AuthErrorMessages.InvalidEmailFormat);

        RuleFor(command => command.Code)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Код верификации"));
    }
}