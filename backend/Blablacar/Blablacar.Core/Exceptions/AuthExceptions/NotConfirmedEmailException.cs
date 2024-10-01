using System.Net;

namespace Blablacar.Core.Exceptions.AuthExceptions;

/// <summary>
/// Если у пользователя не подтвержён Email, но он хочет залогиниться
/// </summary>
public class NotConfirmedEmailException : ApplicationBaseException
{
    /// <inheritdoc />
    public NotConfirmedEmailException(string message,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message, statusCode)
    {
    }

    /// <inheritdoc />
    public NotConfirmedEmailException()
    {
    }
}