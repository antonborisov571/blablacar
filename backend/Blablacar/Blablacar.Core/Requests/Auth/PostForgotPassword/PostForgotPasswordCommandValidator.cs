using Blablacar.Core.Enums;
using FluentValidation;

namespace Blablacar.Core.Requests.Auth.PostForgotPassword;

/// <summary>
/// Валидатор для <see cref="PostForgotPasswordCommand"/>
/// </summary>
public class PostForgotPasswordCommandValidator :
    AbstractValidator<PostForgotPasswordCommand>
{
    /// <inheritdoc />
    public PostForgotPasswordCommandValidator()
    {
        RuleFor(command => command)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Почта"));

        RuleFor(command => command.Email)
            .EmailAddress().WithMessage(AuthErrorMessages.InvalidEmailFormat);
    }
}