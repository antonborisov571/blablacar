using Blablacar.Contracts.Requests.Account.GetUserInfo;
using MediatR;

namespace Blablacar.Core.Requests.Account.GetUserInfo;

/// <summary>
/// Запрос на получение <see cref="GetUserInfoResponse"/>
/// </summary>
public class GetUserInfoQuery : IRequest<GetUserInfoResponse>;