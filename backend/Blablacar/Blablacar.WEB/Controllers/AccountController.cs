using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blablacar.Core.Requests.Account.GetUserInfo;
using Blablacar.Core.Requests.Account.PatchUpdateUserInfo;
using Blablacar.Contracts.Requests.Account.GetUserInfo;
using Blablacar.Contracts.Requests.Account.PatchUpdateTwoFactor;
using Blablacar.Contracts.Requests.Account.PatchUpdateUserInfo;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Requests.Account.PatchUpdateAvatar;
using Blablacar.Core.Requests.Account.PatchUpdateTwoFactor;

namespace Blablacar.WEB.Controllers;

/// <summary>
/// Контроллер отвечающий за действия с аккаунтом
/// </summary>
/// <param name="mediator">Медиатор из библиотеки MediatR</param>
[ApiController]
[Authorize]
[Route("api/[controller]/")]
public class AccountController(IMediator mediator) : ControllerBase
{

    /// <summary>
    /// Возвращает GetUserInfoResponse(Email, UserName)
    /// </summary>
    /// <returns>GetUserInfoResponse(Email, UserName)</returns>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден, или этого CurrentUserId нет</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpGet("UserInfo")]
    [ProducesResponseType(typeof(GetUserInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetUserInfoResponse> UserInfo(CancellationToken cancellationToken)
    {
        var command = new GetUserInfoQuery();
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Обновление данных пользователя
    /// </summary>
    /// <param name="request">PatchUpdateUserInfoRequest(FirstName, LastName, Avatar)</param>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpPatch("UpdateUserInfo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task UpdateUserInfo([FromBody] PatchUpdateUserInfoRequest request)
    {
        var command = new PatchUpdateUserInfoCommand(request);
        await mediator.Send(command);
    }

    /// <summary>
    /// Обновление аватара пользователя
    /// </summary>
    /// <param name="request">IFormFile</param>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpPatch("UpdateAvatar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task UpdateAvatar([FromForm] IFormFile request)
    {
        var command = new PatchUpdateAvatarCommand(request);
        await mediator.Send(command);
    }
    
    /// <summary>
    /// Обновление двухфакторки пользователя
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpPatch("UpdateTwoFactor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task UpdateAvatar(PatchUpdateTwoFactorRequest request)
    {
        var command = new PatchUpdateTwoFactorCommand(request);
        await mediator.Send(command);
    }
}