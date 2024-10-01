using Blablacar.Core.Enums;
using FluentValidation;

namespace Blablacar.Core.Requests.Chat.GetMessages;

/// <summary>
/// Валидатор для <see cref="GetMessagesQuery"/>
/// </summary>
public class GetMessagesQueryValidator : AbstractValidator<GetMessagesQuery>
{
    /// <inheritdoc />
    public GetMessagesQueryValidator()
    {
        RuleFor(command => command.ReceiverId)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Id получателя"));
    }
}