using Blablacar.Contracts.Requests.Chat.GetChats;
using Blablacar.Contracts.Requests.Chat.GetMessages;
using Blablacar.Core.Requests.Chat.GetChats;
using Blablacar.Core.Requests.Chat.GetMessages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blablacar.WEB.Controllers;

/// <summary>
/// Контроллер отвечающий за пользователей
/// </summary>
[ApiController]
[Route("api/[controller]/")]
public class ChatsController(IMediator mediator) : ControllerBase
{

    /// <summary>
    /// Получение сообщений
    /// </summary>
    /// <param name="receiverId">Id получателя</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Сообщения</returns>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь не найден</response>
    /// <response code="404">Если получатель не найден</response>
    [HttpGet("{receiverId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetMessagesResponse> GetMessages(string receiverId, CancellationToken cancellationToken)
    {
        var command = new GetMessagesQuery(receiverId);
        return await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Получение чатов
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Чаты</returns>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpGet("getChats")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetChatsResponse> GetChats(CancellationToken cancellationToken)
    {
        var command = new GetChatsQuery();
        return await mediator.Send(command, cancellationToken);
    }
}