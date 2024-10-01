using FluentValidation;
using Blablacar.Core.Enums;

namespace Blablacar.Core.Requests.Auth.PostResetPassword;

/// <summary>
/// Валидатор для <see cref="PostResetPasswordCommand"/>
/// </summary>
public class PostResetPasswordCommandValidator : AbstractValidator<PostResetPasswordCommand>
{
    /// <inheritdoc />
    public PostResetPasswordCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Почта"));

        RuleFor(command => command.Email)
            .EmailAddress().WithMessage(AuthErrorMessages.InvalidEmailFormat);

        RuleFor(command => command.NewPassword)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Пароль"));

        RuleFor(command => command.NewPassword).MinimumLength(8)
            .WithMessage(AuthErrorMessages.ShortPassword(8));

        RuleFor(command => command.NewPasswordConfirm)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Подтвердить пароль"));

        RuleFor(command => command.NewPassword)
            .Equal(command => command.NewPasswordConfirm).WithMessage(AuthErrorMessages.PasswordIsNotConfirmed);
    }
}