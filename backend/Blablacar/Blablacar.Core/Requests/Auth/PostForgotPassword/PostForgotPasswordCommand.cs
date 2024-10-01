using Blablacar.Contracts.Requests.Auth.PostForgotPassword;
using MediatR;

namespace Blablacar.Core.Requests.Auth.PostForgotPassword;

/// <summary>
/// Команда для того чтобы отправить ссылку для нового пароля
/// </summary>
public class PostForgotPasswordCommand : PostForgotPasswordRequest, IRequest
{
    /// <inheritdoc />
    public PostForgotPasswordCommand(PostForgotPasswordRequest request) : base(request)
    {
    }

    /// <inheritdoc />
    public PostForgotPasswordCommand()
    {
    }
}
