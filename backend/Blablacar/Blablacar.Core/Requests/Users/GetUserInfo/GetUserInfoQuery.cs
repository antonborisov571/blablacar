using Blablacar.Contracts.Requests.Users.GetUserInfo;
using MediatR;

namespace Blablacar.Core.Requests.Users.GetUserInfo;

/// <summary>
/// Команда для получения информации о пользователе
/// </summary>
public class GetUserInfoQuery : IRequest<GetUserInformationResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    public GetUserInfoQuery(string userId)
    {
        Id = userId;
    }
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    public string Id { get; set; }
}