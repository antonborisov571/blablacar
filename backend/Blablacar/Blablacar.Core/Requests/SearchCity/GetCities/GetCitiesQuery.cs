using Blablacar.Contracts.Requests.SearchCity.GetCities;
using MediatR;

namespace Blablacar.Core.Requests.SearchCity.GetCities;

/// <summary>
/// Команда для запроса о поиске город
/// </summary>
/// <param name="request">Запрос с названием города</param>
public class GetCitiesQuery(GetCitiesRequest request) 
    : GetCitiesRequest(request), IRequest<GetCitiesResponse>;