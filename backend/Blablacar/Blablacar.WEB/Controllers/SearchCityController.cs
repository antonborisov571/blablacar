using Blablacar.Contracts.Requests.SearchCity.GetCities;
using Blablacar.Core.Requests.SearchCity.GetCities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blablacar.WEB.Controllers;

/// <summary>
/// Контроллер отвечающий за поиск городов 
/// </summary>
[ApiController]
[Route("api/[controller]/")]
[AllowAnonymous]
public class SearchCityController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Получение городов по названию
    /// </summary>
    /// <param name="request">Запрос на получение городов</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Названия городов</returns>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="500">Не удалось соединиться с КЛАДРом</response>
    [HttpGet("GetCities")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<GetCitiesResponse> GetCities([FromQuery] GetCitiesRequest request, CancellationToken cancellationToken)
    {
        var command = new GetCitiesQuery(request);
        return await mediator.Send(command, cancellationToken);
    }
}