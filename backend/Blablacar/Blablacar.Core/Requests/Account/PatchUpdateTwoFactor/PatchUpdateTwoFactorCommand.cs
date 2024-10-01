using Blablacar.Contracts.Requests.Account.PatchUpdateTwoFactor;
using MediatR;

namespace Blablacar.Core.Requests.Account.PatchUpdateTwoFactor;

/// <summary>
/// Команда на обновление двухфакторной авторизации
/// </summary>
/// <param name="request">Запрос</param>
public class PatchUpdateTwoFactorCommand(PatchUpdateTwoFactorRequest request) 
    : PatchUpdateTwoFactorRequest(request), IRequest;