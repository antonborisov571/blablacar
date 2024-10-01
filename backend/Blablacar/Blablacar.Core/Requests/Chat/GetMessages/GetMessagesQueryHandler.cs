using Blablacar.Contracts.Requests.Chat.GetMessages;
using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions;
using Blablacar.Core.Exceptions.AccountExceptions;
using Blablacar.Core.Exceptions.AuthExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Message = Blablacar.Contracts.Requests.Chat.GetMessages.Message;

namespace Blablacar.Core.Requests.Chat.GetMessages;

/// <summary>
/// Обработчик для <see cref="GetMessagesQuery"/>
/// </summary>
/// <param name="userManager">UserManager из identity</param>
/// <param name="userContext">UserContext для получения id</param>
/// <param name="messagesRepository">Репозиторий сообщений</param>
/// <param name="avatarService">Сервис для работы с аватарками</param>
/// <param name="logger">Логгер</param>
public class GetMessagesQueryHandler(
    UserManager<User> userManager,
    IUserContext userContext,
    AbstractMessagesRepository messagesRepository,
    IAvatarService avatarService,
    ILogger<GetMessagesQueryHandler> logger
    )
    : IRequestHandler<GetMessagesQuery, GetMessagesResponse>
{
    /// <inheritdoc />
    public async Task<GetMessagesResponse> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(GetMessagesQuery));
        
        var ourId = userContext.CurrentUserId;

        if (ourId is null)
            throw new CurrentUserIdNotFound("Нет пользователя");

        var our = await userManager.FindByIdAsync(ourId.ToString()!);

        if (our is null)
            throw new NotFoundUserException($"User with id: {ourId}");
        
        var buddy = await userManager.FindByIdAsync(request.ReceiverId);

        if (buddy == null)
            throw new NotFoundException("Получатель не найден");

        var messages = await messagesRepository.GetMessagesByReceiver(our.Id, buddy.Id);

        buddy.Avatar = await avatarService.GetAvatar(buddy, cancellationToken);
        our.Avatar = await avatarService.GetAvatar(our, cancellationToken);

        var messagesResponse = new List<Message>();

        foreach (var message in messages)
        {
            messagesResponse.Add(new Message
            {
                SenderId = message.SenderId,
                SenderName = message.SenderId == our.Id ? our.FirstName : buddy.FirstName,
                SenderAvatar = message.SenderId == our.Id ? our.Avatar : buddy.Avatar,
                Text = message.Text,
                Dispatch = message.Dispatch
            });
        }
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(GetMessagesQuery), DateTime.Now);

        return new GetMessagesResponse
        {
            Messages = messagesResponse,
            OurId = our.Id,
            BuddyName = buddy.FirstName,
            BuddyAvatar = buddy.Avatar
        };
    }
}