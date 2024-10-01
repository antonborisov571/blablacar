using Blablacar.Contracts.Enums;
using Microsoft.AspNetCore.Http;

namespace Blablacar.Contracts.Requests.Account.PatchUpdateUserInfo;

/// <summary>
/// Запрос на обновление данных о пользователе
/// </summary>
public class PatchUpdateUserInfoRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">PatchUpdateUserInfoRequest</param>
    public PatchUpdateUserInfoRequest(PatchUpdateUserInfoRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        FirstName = request.FirstName;
        LastName = request.LastName;
        PreferencesMusic = request.PreferencesMusic;
        PreferencesSmoking = request.PreferencesSmoking;
        PreferencesTalk = request.PreferencesTalk;
        PreferencesAnimal = request.PreferencesAnimal;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public PatchUpdateUserInfoRequest()
    {
    } 
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? FirstName { get; set; }
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string? LastName { get; set; }
    
    /// <summary>
    /// Предпочтения о музыке
    /// </summary>
    public MusicType? PreferencesMusic { get; set; }

    /// <summary>
    /// Предпочтения о курении
    /// </summary>
    public SmokingType? PreferencesSmoking { get; set; }

    /// <summary>
    /// Разговорчивость
    /// </summary>
    public TalkType? PreferencesTalk { get; set; }

    /// <summary>
    /// Предпочтения о животных
    /// </summary>
    public AnimalType? PreferencesAnimal { get; set; }
}