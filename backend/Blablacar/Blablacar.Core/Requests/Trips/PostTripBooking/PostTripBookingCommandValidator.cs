using Blablacar.Core.Enums;
using FluentValidation;

namespace Blablacar.Core.Requests.Trips.PostTripBooking;

/// <summary>
/// Валидатор для <see cref="PostTripBookingCommand"/>
/// </summary>
public class PostTripBookingCommandValidator : AbstractValidator<PostTripBookingCommand>
{
    /// <inheritdoc />
    public PostTripBookingCommandValidator()
    {
        RuleFor(command => command)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Запрос"));

        RuleFor(command => command.TripId)
            .NotEmpty().WithMessage(BlablacarErrorMessages.EmptyField("Id поездки"));
    }
}