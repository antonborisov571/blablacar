using Blablacar.Core.Enums;
using FluentValidation;

namespace Blablacar.Core.Requests.Trips.PostCreateTrip;

/// <summary>
/// Валидатор для <see cref="PostCreateTripCommand"/>
/// </summary>
public class PostCreateTripCommandValidator 
    : AbstractValidator<PostCreateTripCommand>
{
    /// <inheritdoc />
    public PostCreateTripCommandValidator()
    {
        RuleFor(command => command)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Запрос"));

        RuleFor(command => command.CountPassengers)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("CountPassengers"))
            .LessThanOrEqualTo(8).WithMessage(BlablacarErrorMessages.GreaterThanEight)
            .GreaterThanOrEqualTo(1).WithMessage(BlablacarErrorMessages.LessThanOne);

        RuleFor(command => command.DateTimeTrip)
            .Must(BlablacarErrorMessages.IsDateValid)
            .WithMessage(BlablacarErrorMessages.DateLessThanNow);

        RuleFor(command => command.WhereFrom)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Откуда"));
        
        RuleFor(command => command.Where)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Куда"));
    }
}