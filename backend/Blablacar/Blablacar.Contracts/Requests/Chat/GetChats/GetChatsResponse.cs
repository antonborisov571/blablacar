namespace Blablacar.Contracts.Requests.Chat.GetChats;

/// <summary>
/// Ответ на запрос о получении чатов
/// </summary>
public class GetChatsResponse
{
    /// <summary>
    /// Чаты
    /// </summary>
    public List<Chat> Chats { get; set; } = new();
}