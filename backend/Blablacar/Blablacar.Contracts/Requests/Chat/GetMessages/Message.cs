namespace Blablacar.Contracts.Requests.Chat.GetMessages;

/// <summary>
/// Сообщение
/// </summary>
public class Message
{
    /// <summary>
    /// Id отправителя
    /// </summary>
    public string SenderId { get; set; } = default!;

    /// <summary>
    /// Имя отправителя
    /// </summary>
    public string SenderName { get; set; } = default!;

    /// <summary>
    /// Аватар отправителя
    /// </summary>
    public string? SenderAvatar { get; set; }

    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string Text { get; set; } = default!;
    
    /// <summary>
    /// Время отправки
    /// </summary>
    public DateTime Dispatch { get; set; }
}