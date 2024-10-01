namespace Blablacar.Contracts.Requests.Chat.GetChats;

/// <summary>
/// Чат
/// </summary>
public class Chat
{
    /// <summary>
    /// Id собеседника
    /// </summary>
    public string BuddyId { get; set; } = default!;
    
    /// <summary>
    /// Имя собеседника
    /// </summary>
    public string BuddyName { get; set; } = default!;
    
    /// <summary>
    /// Аватар собеседника
    /// </summary>
    public string? BuddyAvatar { get; set; }
}