namespace Blablacar.Core.Models;

/// <summary>
/// Настройки для КЛАДР
/// </summary>
public class SearchCityOptions
{
    /// <summary>
    /// Host Сервиса для поиска города
    /// </summary>
    public string Host { get; set; } = default!;

    /// <summary>
    /// Токен для доступа к сервису
    /// </summary>
    public string Token { get; set; } = default!;
}