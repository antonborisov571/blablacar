using Blablacar.Core.Enums;
using FluentValidation;

namespace Blablacar.Core.Requests.Auth.PostRegister;

/// <summary>
/// Валидатор для <see cref="PostRegisterCommand"/>
/// </summary>
public class PostRegisterCommandValidator : AbstractValidator<PostRegisterCommand>
{
    /// <inheritdoc />
    public PostRegisterCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Почта"));

        RuleFor(command => command.Email).
            EmailAddress().WithMessage(AuthErrorMessages.InvalidEmailFormat);
        
        RuleFor(command => command.FirstName)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Имя"));
        
        RuleFor(command => command.LastName)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Фамилия"));
        
        RuleFor(command => command.Password)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Пароль")); 
        
        RuleFor(command => command.Password).MinimumLength(8)
            .WithMessage(AuthErrorMessages.ShortPassword(8));
        
        RuleFor(command => command.PasswordConfirm)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Повторить пароль"));  
            
        RuleFor(command => command.Password)
            .Equal(command => command.PasswordConfirm).WithMessage(AuthErrorMessages.PasswordIsNotConfirmed);
    }
}