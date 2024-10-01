using Blablacar.Contracts.Requests.Chat.GetChats;
using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions;
using Blablacar.Core.Exceptions.AccountExceptions;
using Blablacar.Core.Exceptions.AuthExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.Chat.GetChats;

/// <summary>
/// Обработчик для <see cref="GetChatsQuery"/>
/// </summary>
/// <param name="userContext">UserContext для получения id пользователя</param>
/// <param name="userManager">UserManager из Identity</param>
/// <param name="avatarService">Сервис для работы с аватарками</param>
/// <param name="messagesRepository">Репозиторий сообщений</param>
/// <param name="logger">Логгер</param>
public class GetChatsQueryHandler(
    IUserContext userContext,
    UserManager<User> userManager,
    AbstractMessagesRepository messagesRepository,
    IAvatarService avatarService,
    ILogger<GetChatsQueryHandler> logger
    ) : IRequestHandler<GetChatsQuery, GetChatsResponse>
{
    /// <inheritdoc />
    public async Task<GetChatsResponse> Handle(GetChatsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(GetChatsQuery));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("Нет пользователя");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"User with id: {userId}");

        var messages = messagesRepository.GetChats(user.Id);

        var chats = new List<Contracts.Requests.Chat.GetChats.Chat>();

        foreach (var message in messages)
        {
            chats.Add(new Contracts.Requests.Chat.GetChats.Chat
            {
                BuddyId = message.SenderId == user.Id ? message.ReceiverId : message.SenderId,
                BuddyName = message.SenderId == user.Id ? message.Receiver.FirstName : message.Sender.FirstName,
                BuddyAvatar = message.SenderId == user.Id 
                    ? await avatarService.GetAvatar(message.Receiver, cancellationToken)
                    : await avatarService.GetAvatar(message.Sender, cancellationToken)
            });
        }
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(GetChatsQuery), DateTime.Now);

        return new GetChatsResponse
        {
            Chats = chats.DistinctBy(x => x.BuddyId).ToList()
        };
    }
}