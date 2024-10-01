using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using Blablacar.Contracts.Requests.SearchCity.GetCities;
using Blablacar.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blablacar.Core.Requests.SearchCity.GetCities;


/// <summary>
/// Обработчик для <see cref="GetCitiesQuery"/>
/// </summary>
/// <param name="httpClientFactory">HttpClient factory</param>
/// <param name="options">Настройки подключения</param>
/// <param name="logger">Логгер</param>
public class GetCitiesQueryHandler(
    IHttpClientFactory httpClientFactory,
    SearchCityOptions options,
    ILogger<GetCitiesQueryHandler> logger
    ) : IRequestHandler<GetCitiesQuery, GetCitiesResponse>
{
    /// <inheritdoc />
    public async Task<GetCitiesResponse> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(GetCitiesQuery));
        
        var uri = new UriBuilder(options.Host);
        var query = HttpUtility.ParseQueryString(uri.Query);
        
        query.Add("token", options.Token);
        query.Add("query", request.CityName);
        query.Add("contentType", "city");
        query.Add("limit", "5");
        
        uri.Query = query.ToString();
        
        using var client = httpClientFactory.CreateClient();

        var response = await client.GetAsync(uri.ToString(), cancellationToken);
        
        var responseContent = await response.Content.ReadAsStreamAsync(cancellationToken);

        var cityResponse =
            await JsonSerializer.DeserializeAsync<CityResponse>(
                responseContent,
                new JsonSerializerOptions{PropertyNameCaseInsensitive = true},
                cancellationToken);
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(GetCitiesQuery), DateTime.Now);
        
        
        return new GetCitiesResponse
        {
            CitiesName = cityResponse != null 
                ? cityResponse.Result
                    .Select(city => city.Name).Distinct().Skip(1).ToList()
                : new List<string>()
        };
    }
}

/// <summary>
/// Класс города для десериализации
/// </summary>
public class City
{
    /// <summary>
    /// Имя города
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Тип города
    /// </summary>
    public string Type { get; set; } = default!;
}

/// <summary>
/// Ответ от сервиса 
/// </summary>
public class CityResponse
{
    /// <summary>
    /// Результат с городами
    /// </summary>
    public List<City> Result { get; set; } = new();
}