using Blablacar.Core.Enums;
using FluentValidation;

namespace Blablacar.Core.Requests.Trips.GetTrips;

/// <summary>
/// Валидатор для <see cref="GetTripsQuery"/>
/// </summary>
public class GetTripsQueryValidator : AbstractValidator<GetTripsQuery>
{
    /// <inheritdoc />
    public GetTripsQueryValidator()
    {
        RuleFor(command => command)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Команда"));

        RuleFor(command => command.WhereFrom)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Откуда"));

        RuleFor(command => command.Where)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Куда"));

        RuleFor(command => command.CountPassengers)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("CountPassengers"))
            .LessThanOrEqualTo(8).WithMessage(BlablacarErrorMessages.GreaterThanEight)
            .GreaterThanOrEqualTo(1).WithMessage(BlablacarErrorMessages.LessThanOne);

        RuleFor(command => command.DateTimeTrip)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("DateTimeTrip"))
            .Must(BlablacarErrorMessages.IsDateValid);

    }
}