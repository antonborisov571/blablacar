using AutoMapper;
using Blablacar.Contracts.Requests.Trips.GetTrips;
using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Models;
using Blablacar.Core.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.Trips.GetTrips;


/// <summary>
/// Обработчик для <see cref="GetTripsQuery"/>
/// </summary>
/// <param name="tripsRepository">Репозиторий поездок</param>
/// <param name="mapper">Маппер</param>
/// <param name="avatarService">Аватар сервис</param>
/// <param name="logger">Логгер</param>
public class GetTripsQueryHandler(
    AbstractTripsRepository tripsRepository,
    IMapper mapper,
    IAvatarService avatarService,
    ILogger<GetTripsQueryHandler> logger
        ) : IRequestHandler<GetTripsQuery, GetTripsResponse>
{
    /// <inheritdoc />
    public async Task<GetTripsResponse> Handle(GetTripsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(GetTripsQuery));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        
        var trips = await tripsRepository.GetTripsByRequest(request);
        
        foreach (var driver in trips.Select(t => t.Driver).DistinctBy(d => d.Id))
        {
            driver.Avatar = await avatarService.GetAvatar(driver, cancellationToken);
        }
        
        var responseItems = mapper.Map<List<GetTripsResponseItem>>(trips);
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(GetTripsQuery), DateTime.Now);
        
        return new GetTripsResponse(responseItems);
    }
}