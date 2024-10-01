using AutoMapper;
using Blablacar.Contracts.Requests.Trips.GetTrip;
using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.Trips.GetTrip;


/// <summary>
/// Обработчик для <see cref="GetTripQuery"/>
/// </summary>
/// <param name="tripsRepository">Репозиторий поездок</param>
/// <param name="mapper">Маппер</param>
/// <param name="avatarService">Сервис для работы с аватарками</param>
/// <param name="userContext">UserContext для получения id пользователя</param>
/// <param name="userManager">UserManager из Identity</param>
/// <param name="logger">Логгер</param>
public class GetTripQueryHandler(
    AbstractTripsRepository tripsRepository,
    IMapper mapper,
    IAvatarService avatarService,
    IUserContext userContext,
    UserManager<User> userManager,
    ILogger<GetTripQueryHandler> logger) 
    : IRequestHandler<GetTripQuery, GetTripResponse>
{
    /// <inheritdoc />
    public async Task<GetTripResponse> Handle(GetTripQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(GetTripQuery));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        
        var trip = await tripsRepository.GetTripsWithDriverPassengers(request.TripId);

        if (trip == null)
            throw new NotFoundException("Поездка не найдена");
        
        foreach (var passenger in trip.Passengers.DistinctBy(d => d.Id))
        {
            passenger.Avatar = await avatarService.GetAvatar(passenger, cancellationToken);
        }

        trip.Driver.Avatar = await avatarService.GetAvatar(trip.Driver, cancellationToken);
        
        var tripDto = mapper.Map<GetTripResponse>(trip);
        
        var userId = userContext.CurrentUserId;
        
        if (userId == null)
            return tripDto;

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user == null)
            return tripDto;

        tripDto.IsDriver = user.Id == trip.Driver.Id;
        tripDto.IsPassenger = trip.Passengers.Any(x => x.Id == user.Id);
        tripDto.IsActive = trip.DateTimeTrip > DateTime.Now;
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(GetTripQuery), DateTime.Now);
        
        return tripDto;
    }
}