using Blablacar.Contracts.Enums;

namespace Blablacar.Contracts.Requests.Account.GetUserInfo;

/// <summary>
/// Ответ для запроса на получение информации о пользователе
/// </summary>
public class GetUserInfoResponse
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string FirstName { get; set; } = default!;
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Дата рождения пользователя 
    /// </summary>
    public DateTime Birthday { get; set; } = default!;
    
    /// <summary>
    /// Дата регистрации пользователя
    /// </summary>
    public DateTime DateRegistration { get; set; } = default!;
    
    /// <summary>
    /// Ссылка на аватар пользователя
    /// </summary>
    public string? Avatar { get; set; } = default!;

    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Предпочтения о музыке
    /// </summary>
    public MusicType PreferencesMusic { get; set; }

    /// <summary>
    /// Предпочтения о курении
    /// </summary>
    public SmokingType PreferencesSmoking { get; set; }

    /// <summary>
    /// Разговорчивость
    /// </summary>
    public TalkType PreferencesTalk { get; set; }

    /// <summary>
    /// Предпочтения о животных
    /// </summary>
    public AnimalType PreferencesAnimal { get; set; }
    
    /// <summary>
    /// Включена ли двухфакторка
    /// </summary>
    public bool TwoFactorEnabled { get; set; }
}