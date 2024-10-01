using Blablacar.Contracts.Requests.Users.GetUserInfo;
using Blablacar.Core.Requests.Users.GetUserInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blablacar.WEB.Controllers;

/// <summary>
/// Контроллер отвечающий за пользователей
/// </summary>
[ApiController]
[Route("api/[controller]/")]
public class UsersController(IMediator mediator) : ControllerBase
{
    
    /// <summary>
    /// Получить пользователь по id
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Информация о пользователе</returns>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь не найден</response>
    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetUserInformationResponse> GetUserInfo(
        string userId,
        CancellationToken cancellationToken)
    {
        var command = new GetUserInfoQuery(userId);
        return await mediator.Send(command, cancellationToken);
    }
}