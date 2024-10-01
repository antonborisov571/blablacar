using System.Net;

namespace Blablacar.Core.Exceptions.OAuthAccountExceptions;

/// <summary>
/// Если после авторизации через сторонний сервис не найдено ExternalLoginInfo
/// </summary>
public class ExternalLoginInfoNotFoundException : ApplicationBaseException
{
    /// <inheritdoc />
    public ExternalLoginInfoNotFoundException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode)

    {
    }

    /// <inheritdoc />
    public ExternalLoginInfoNotFoundException()
    {
    }
}