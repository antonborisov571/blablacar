namespace Blablacar.Contracts.Requests.Chat.SendMessage;

/// <summary>
/// Отправка сообщения
/// </summary>
public class SendMessageRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public SendMessageRequest(SendMessageRequest request)
    {
        ReceiverId = request.ReceiverId;
        Text = request.Text;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public SendMessageRequest()
    {
    }
    
    /// <summary>
    /// Id получателя
    /// </summary>
    public string ReceiverId { get; set; } = default!;

    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string Text { get; set; } = default!;
}