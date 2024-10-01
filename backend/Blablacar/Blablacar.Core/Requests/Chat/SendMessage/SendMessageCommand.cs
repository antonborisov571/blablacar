using Blablacar.Contracts.Requests.Chat.SendMessage;
using Blablacar.Core.Entities;
using MediatR;

namespace Blablacar.Core.Requests.Chat.SendMessage;

/// <summary>
/// Команда для отправки сообщений
/// </summary>
/// <param name="request">Запрос</param>
public class SendMessageCommand(SendMessageRequest request)
    : SendMessageRequest(request), IRequest<SendMessageResponse>
{
    /// <summary>
    /// Id отправителя
    /// </summary>
    public string SenderId { get; set; } = default!;
}