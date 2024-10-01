using Blablacar.Contracts.Requests.Auth.PostTwoFactor;
using MediatR;

namespace Blablacar.Core.Requests.Auth.PostTwoFactor;

/// <summary>
/// Команда для двухфакторки
/// </summary>
/// <param name="request"></param>
public class PostTwoFactorCommand(PostTwoFactorRequest request)
    : PostTwoFactorRequest(request), IRequest<PostTwoFactorResponse>;