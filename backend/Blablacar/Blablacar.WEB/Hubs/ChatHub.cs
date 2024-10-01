using Blablacar.Contracts.Requests.Chat.SendMessage;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions;
using Blablacar.Core.Exceptions.AccountExceptions;
using Blablacar.Core.Exceptions.AuthExceptions;
using Blablacar.Core.Exceptions.ChatExceptions;
using Blablacar.Core.Requests.Chat.SendMessage;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Blablacar.WEB.Hubs;

/// <inheritdoc />
public class ChatHub(
    IMediator mediator,
    UserManager<User> userManager
    ) : Hub
{
    
    private static readonly Dictionary<string, string> UserConnections = new();

    /// <inheritdoc />
    public override async Task OnConnectedAsync()
    {
        var context = Context.GetHttpContext();
        if (context == null)
            throw new NotFoundContextException("Не найден HttpContext");
        
        var user =  await userManager.GetUserAsync(context.User);
        
        if (user is null)
            throw new NotFoundUserException($"Пользователь не найден");
        
        UserConnections[user.Id] = Context.ConnectionId;
        
        await base.OnConnectedAsync();
    }

    /// <inheritdoc />
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        UserConnections.Remove(UserConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key);
        
        await base.OnDisconnectedAsync(exception);
    }

    /// <summary>
    /// Отправка сообщения
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <exception cref="CurrentUserIdNotFound">Если пользователь не найден</exception>
    public async Task SendMessage(SendMessageRequest request)
    {
        var userId = Context.UserIdentifier;
        
        if (userId is null)
            throw new CurrentUserIdNotFound("Нет пользователя");
        
        var command = new SendMessageCommand(request) {SenderId = userId};

        var message = await mediator.Send(command);
        
        var connectionReceiverId = UserConnections.FirstOrDefault(x => x.Key == request.ReceiverId).Value;
        
        var connectionSenderId = UserConnections.FirstOrDefault(x => x.Key == userId).Value;
        
        await Clients.Clients(connectionReceiverId, connectionSenderId).SendAsync("ReceiveMessage", message);
    }
}