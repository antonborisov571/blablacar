using System.Globalization;
using Blablacar.Contracts.Requests.Chat.SendMessage;
using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions;
using Blablacar.Core.Exceptions.AccountExceptions;
using Blablacar.Core.Exceptions.AuthExceptions;
using Blablacar.Core.Extensions;
using Blablacar.Core.Models;
using Blablacar.Core.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.Chat.SendMessage;

/// <summary>
/// Обработчик для <see cref="SendMessageCommand"/>
/// </summary>
/// <param name="userManager">UserManager из identity</param>
/// <param name="messagesRepository">Репозиторий сообщений</param>
/// <param name="avatarService">Сервис для работы с фотографиями</param>
/// <param name="emailSender">Сервис отправки email-сообщений</param>
/// <param name="logger">Логгер</param>
public class SendMessageCommandHandler(
    UserManager<User> userManager,
    AbstractMessagesRepository messagesRepository,
    IAvatarService avatarService,
    IEmailSender emailSender,
    ILogger<SendMessageCommandHandler> logger
    )
    : IRequestHandler<SendMessageCommand, SendMessageResponse>
{
    /// <inheritdoc />
    public async Task<SendMessageResponse> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(SendMessageCommand));
        
        if (request.SenderId is null)
            throw new CurrentUserIdNotFound("Нет пользователя");
        
        var sender = await userManager.FindByIdAsync(request.SenderId);

        if (sender is null)
            throw new NotFoundUserException("Не найден пользователь с таким id");

        var receiver = await userManager.FindByIdAsync(request.ReceiverId);

        if (receiver is null)
            throw new NotFoundUserException("Не найден пользователь с таким id");
        
        var time = DateTime.Now;
        
        var message = new Message
        {
            SenderId = sender.Id,
            ReceiverId = request.ReceiverId,
            Text = request.Text,
            Dispatch = time
        };
        
        await messagesRepository.AddAsync(message);
        
        var messageTemplate =
            await EmailTemplateHelper.GetEmailTemplateAsync(Templates.SendMessageNotification,
                cancellationToken);
        
        var placeholders = new Dictionary<string, string>
        {
            ["{buddyName}"] = receiver.FirstName,
            ["{text}"] = request.Text,
            ["{dispatch}"] = time.ToString("f", CultureInfo.CurrentCulture)
        };
        
        var messageMail = messageTemplate.ReplacePlaceholders(placeholders);
        
        await emailSender.SendEmailAsync(receiver.Email ?? "",
            messageMail, cancellationToken);

        var response = new SendMessageResponse
        {
            SenderId = sender.Id,
            SenderName = sender.FirstName,
            SenderAvatar = await avatarService.GetAvatar(sender, cancellationToken),
            Text = request.Text,
            Dispatch = time
        };
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(SendMessageCommand), DateTime.Now);

        return response;
    }
}