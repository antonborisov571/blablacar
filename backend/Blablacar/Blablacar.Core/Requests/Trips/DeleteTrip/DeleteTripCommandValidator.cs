using Blablacar.Core.Enums;
using FluentValidation;

namespace Blablacar.Core.Requests.Trips.DeleteTrip;

/// <summary>
/// Валидатор для <see cref="DeleteTripCommand"/>
/// </summary>
public class DeleteTripCommandValidator : AbstractValidator<DeleteTripCommand>
{
    /// <inheritdoc />
    public DeleteTripCommandValidator()
    {
        RuleFor(command => command)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Запрос"));

        RuleFor(command => command.TripId)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Id поездки"));
    }
}