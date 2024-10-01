using Blablacar.Core.Enums;
using FluentValidation;

namespace Blablacar.Core.Requests.Users.GetUserInfo;

/// <summary>
/// Валидатор для <see cref="GetUserInfoQuery"/>
/// </summary>
public class GetUserInfoQueryValidator 
    : AbstractValidator<GetUserInfoQuery>
{
    /// <inheritdoc />
    public GetUserInfoQueryValidator()
    {
        RuleFor(command => command)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Запрос"));

        RuleFor(command => command.Id)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Id"));
    }
}