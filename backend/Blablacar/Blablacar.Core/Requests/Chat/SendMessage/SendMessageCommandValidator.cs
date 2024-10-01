using Blablacar.Core.Enums;
using FluentValidation;

namespace Blablacar.Core.Requests.Chat.SendMessage;

/// <summary>
/// Валидатор для <see cref="SendMessageCommand"/>
/// </summary>
public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    /// <inheritdoc />
    public SendMessageCommandValidator()
    {
        RuleFor(command => command.ReceiverId)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Id получателя"));

        RuleFor(command => command.SenderId)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Id отправителя"));

        RuleFor(command => command.Text)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Текст сообщения"));
    }
}