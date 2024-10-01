using System.Text.Json.Serialization;

namespace Blablacar.Contracts.Requests.SearchCity.GetCities;

/// <summary>
/// Ответ на запрос <see cref="GetCitiesRequest"/>
/// </summary>
public class GetCitiesResponse
{
    /// <summary>
    /// Название городов
    /// </summary>
    public List<string> CitiesName { get; set; } = default!;
}