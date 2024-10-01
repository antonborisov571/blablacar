using FluentValidation;

namespace Blablacar.Core.Requests.Account.PatchUpdateUserInfo;

/// <summary>
/// Валидатор для <see cref="PatchUpdateUserInfoCommand"/>
/// </summary>
public class PatchUpdateUserInfoCommandValidator : AbstractValidator<PatchUpdateUserInfoCommand>
{
    /// <inheritdoc />
    public PatchUpdateUserInfoCommandValidator()
    {
    }
}