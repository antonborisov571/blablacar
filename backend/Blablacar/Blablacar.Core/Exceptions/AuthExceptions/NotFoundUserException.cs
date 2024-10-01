using System.Net;

namespace Blablacar.Core.Exceptions.AuthExceptions;

/// <summary>
/// Исключение выбрасывается при логине пользователя, если не удалось найти пользователя с нужной почтой
/// </summary>
public class NotFoundUserException : ApplicationBaseException
{
    /// <inheritdoc />
    public NotFoundUserException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode)
    {
    }

    /// <inheritdoc />
    public NotFoundUserException()
    {
    }
}