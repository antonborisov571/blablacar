using Blablacar.Contracts.Requests.Auth.PostConfirmEmail;
using MediatR;

namespace Blablacar.Core.Requests.Auth.PostConfirmEmail;

/// <summary>
/// Команда для подтверждения почты пользователя
/// </summary>
public class PostConfirmEmailCommand : PostConfirmEmailRequest, IRequest
{
    /// <inheritdoc />
    public PostConfirmEmailCommand(PostConfirmEmailRequest request) : base(request)
    {
    }

    /// <inheritdoc />
    public PostConfirmEmailCommand()
    {
    }
}