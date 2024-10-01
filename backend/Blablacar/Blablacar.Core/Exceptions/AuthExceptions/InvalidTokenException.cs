using System.Net;

namespace Blablacar.Core.Exceptions.AuthExceptions;

/// <summary>
/// Если пришёл не валидный JWT или Refresh Token
/// </summary>
public class InvalidTokenException : ApplicationBaseException
{
    /// <inheritdoc />
    public InvalidTokenException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
        : base(message, statusCode)
    {
    }

    /// <inheritdoc />
    public InvalidTokenException()
    {
    }
}