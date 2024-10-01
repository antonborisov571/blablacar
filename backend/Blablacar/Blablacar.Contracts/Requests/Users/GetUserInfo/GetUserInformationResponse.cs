using Blablacar.Contracts.Enums;

namespace Blablacar.Contracts.Requests.Users.GetUserInfo;

/// <summary>
/// Ответ на запрос о получении пользователя
/// </summary>
public class GetUserInformationResponse
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public string Id { get; set; } = default!;
    
    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Аватар
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime Birthday { get; set; }
    
    /// <summary>
    /// Дата регистрации
    /// </summary>
    public DateTime DateRegistration { get; set; } 
    
    /// <summary>
    /// Количество поездок
    /// </summary>
    public int CountTrips { get; set; }
    
    /// <summary>
    /// Предпочтения о курении
    /// </summary>
    public SmokingType PreferencesSmoking { get; set; }
    
    /// <summary>
    /// Предпочтения о музыке
    /// </summary>
    public MusicType PreferencesMusic { get; set; }
    
    /// <summary>
    /// Предпочтения о животных 
    /// </summary>
    public AnimalType PreferencesAnimal { get; set; }
    
    /// <summary>
    /// Насколько болтлив пользователь
    /// </summary>
    public TalkType PreferencesTalk { get; set; }
}