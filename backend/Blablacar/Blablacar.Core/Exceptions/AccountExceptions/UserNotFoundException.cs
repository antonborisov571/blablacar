using System.Net;

namespace Blablacar.Core.Exceptions.AccountExceptions;

/// <summary>
/// Если user не найден
/// </summary>
public class UserNotFoundException : ApplicationBaseException
{
    /// <inheritdoc />
    public UserNotFoundException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode)
    {
    }

    /// <inheritdoc />
    public UserNotFoundException()
    {
    }
}