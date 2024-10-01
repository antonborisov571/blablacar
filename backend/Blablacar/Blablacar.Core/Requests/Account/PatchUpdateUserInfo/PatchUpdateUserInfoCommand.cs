using MediatR;
using Blablacar.Contracts.Requests.Account.PatchUpdateUserInfo;

namespace Blablacar.Core.Requests.Account.PatchUpdateUserInfo;

/// <summary>
/// Запрос на обновление данных о пользователе
/// </summary>
public class PatchUpdateUserInfoCommand : PatchUpdateUserInfoRequest, IRequest<PatchUpdateUserInfoResponse> 
{
    /// <inheritdoc />
    public PatchUpdateUserInfoCommand(PatchUpdateUserInfoRequest request) : base(request)
    {
    }
}