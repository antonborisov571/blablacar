namespace Blablacar.Contracts.Requests.Chat.GetMessages;

/// <summary>
/// Ответ на запрос о получение сообщений
/// </summary>
public class GetMessagesResponse
{
    /// <summary>
    /// Сообщения
    /// </summary>
    public List<Message> Messages { get; set; } = new();

    /// <summary>
    /// Id кто хочет получить сообщения
    /// </summary>
    public string OurId { get; set; } = default!;

    /// <summary>
    /// Имя собеседника
    /// </summary>
    public string BuddyName { get; set; } = default!;
    
    /// <summary>
    /// Аватар собеседника
    /// </summary>
    public string? BuddyAvatar { get; set; }
}