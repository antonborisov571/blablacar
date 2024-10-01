using System.Net;

namespace Blablacar.Core.Exceptions.AuthExceptions;

/// <summary>
/// Ошибка выбрасывается, если UserManager не смог по каким-то причинам зарегистрировать пользователя
/// </summary>
public class RegisterUserException : ApplicationBaseException
{
    /// <inheritdoc />
    public RegisterUserException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message, statusCode)
    {
    }

    /// <inheritdoc />
    public RegisterUserException()
    {
    }
}