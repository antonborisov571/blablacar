using Blablacar.Core.Enums;
using FluentValidation;

namespace Blablacar.Core.Requests.Trips.PostCreateTripNotification;

/// <summary>
/// Валидатор для <see cref="PostCreateTripNotificationCommand"/>
/// </summary>
public class PostCreateTripNotificationCommandValidator 
    : AbstractValidator<PostCreateTripNotificationCommand>
{
    /// <inheritdoc />
    public PostCreateTripNotificationCommandValidator()
    {
        RuleFor(command => command)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Запрос"));

        RuleFor(command => command.DateTimeTrip)
            .Must(BlablacarErrorMessages.IsDateValid)
            .WithMessage(BlablacarErrorMessages.DateLessThanNow);

        RuleFor(command => command.WhereFrom)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Откуда"));
        
        RuleFor(command => command.Where)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Куда"));
    }
}