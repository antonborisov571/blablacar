namespace Blablacar.Contracts.Requests.SearchCity.GetCities;

/// <summary>
/// Запрос для поиска городов
/// </summary>
public class GetCitiesRequest
{

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public GetCitiesRequest(GetCitiesRequest request)
    {
        CityName = request.CityName;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public GetCitiesRequest()
    {
    }
    
    /// <summary>
    /// Название города
    /// </summary>
    public string CityName { get; set; } = default!;
}