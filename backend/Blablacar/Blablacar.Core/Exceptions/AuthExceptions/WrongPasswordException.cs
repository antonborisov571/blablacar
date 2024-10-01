using System.Net;

namespace Blablacar.Core.Exceptions.AuthExceptions;

/// <summary>
/// Исключение выбрасывается при логине пользователя, если он ввёл неверный пароль от учётной записи
/// </summary>
public class WrongPasswordException : ApplicationBaseException
{
    /// <inheritdoc />
    public WrongPasswordException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode)
    {
    }

    /// <inheritdoc />
    public WrongPasswordException()
    {
    }
}