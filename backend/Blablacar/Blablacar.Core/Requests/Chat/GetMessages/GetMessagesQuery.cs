using Blablacar.Contracts.Requests.Chat.GetMessages;
using MediatR;

namespace Blablacar.Core.Requests.Chat.GetMessages;

/// <summary>
/// Команда для получения сообщений
/// </summary>
public class GetMessagesQuery : IRequest<GetMessagesResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="receiverId">Id получателя</param>
    public GetMessagesQuery(string receiverId)
    {
        ReceiverId = receiverId;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public GetMessagesQuery()
    {
    }

    /// <summary>
    /// Id получателя
    /// </summary>
    public string ReceiverId { get; set; } = default!;
}