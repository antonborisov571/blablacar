using MediatR;
using Blablacar.Contracts.Requests.Auth.PostResetPassword;

namespace Blablacar.Core.Requests.Auth.PostResetPassword;

/// <summary>
/// Команда для сброса пароля
/// </summary>
public class PostResetPasswordCommand : PostResetPasswordRequest, IRequest<PostResetPasswordResponse>
{
    /// <inheritdoc />
    public PostResetPasswordCommand(PostResetPasswordRequest request) : base(request)
    {
    }
}