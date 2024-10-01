using Blablacar.Core.Enums;
using FluentValidation;

namespace Blablacar.Core.Requests.Trips.DeletePassenger;

/// <summary>
/// Валидатор для <see cref="DeletePassengerCommand"/>
/// </summary>
public class DeletePassengerCommandValidator : AbstractValidator<DeletePassengerCommand>
{
    /// <inheritdoc />
    public DeletePassengerCommandValidator()
    {
        RuleFor(command => command)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Команда"));

        RuleFor(command => command.PassengerId)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Id пассажира"));

        RuleFor(command => command.TripId)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Id поездки"));
    }
}