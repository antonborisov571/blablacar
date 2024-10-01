using System.ComponentModel.DataAnnotations;
using Blablacar.Contracts.Enums;
using Microsoft.AspNetCore.Identity;

namespace Blablacar.Core.Entities;

/// <summary>
/// Сущность пользователя
/// </summary>
public class User : IdentityUser, IEntity<string>
{
    /// <summary>
    /// JWT
    /// </summary>
    public string? AccessToken { get; set; }
    
    /// <summary>
    /// Токен для обновления JWT
    /// </summary>
    public string? RefreshToken { get; set; }
    
    /// <summary>
    /// Время жизни Refresh Token
    /// </summary>
    public DateTime? RefreshTokenExpiryTime { get; set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string LastName { get; set; } = default!;
    
    /// <summary>
    /// Ссылка на аватар пользователя
    /// </summary>
    [Url]
    public string? Avatar { get; set; } 
    
    /// <summary>
    /// День рождения пользователя
    /// </summary>
    public DateTime Birthday { get; set; }
    
    /// <summary>
    /// Дата регистрации
    /// </summary>
    public DateTime DateRegistration { get; set; }
    
    /// <summary>
    /// Телефон пользователя
    /// </summary>
    public string? Phone { get; protected set; }

    /// <summary>
    /// Подтвержден
    /// </summary>
    public bool IsConfirmed { get; protected set; }
    
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
    /// Поездки, в которых пользователь был водителем
    /// </summary>
    public List<Trip> DriverTrips { get; set; } = new();

    /// <summary>
    /// Поездки, в которых пользователь был пассажиром
    /// </summary>
    public List<Trip> PassengerTrips { get; set; } = new();
    
    /// <summary>
    /// Уведомления о поездках
    /// </summary>
    public List<TripNotification> TripNotifications { get; set; } = new();

    /// <summary>
    /// Создать тестовую сущность
    /// </summary>
    /// <param name="id">Ид пользователя</param>
    /// <param name="login">Логин пользователя</param>
    /// <param name="birthday">Дата рождения</param>
    /// <param name="email">E-mail пользователя</param>
    /// <param name="phone">Телефон</param>
    /// <param name="passwordHash">Хеш пароля</param>
    /// <returns></returns>
    [Obsolete("Только для тестов")]
    public static User CreateForTest(
        Guid? id = default,
        string login = default!,
        DateTime birthday = default,
        string email = default!,
        string phone = default!,
        string? passwordHash = default)
        => new()
        {
            Id = id.ToString() ?? Guid.NewGuid().ToString(),
            Birthday = birthday,
            Email = email,
            Phone = phone,
            PasswordHash = passwordHash
        };
}