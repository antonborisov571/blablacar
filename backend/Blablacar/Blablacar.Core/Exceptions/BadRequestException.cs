using System.Net;

namespace Blablacar.Core.Exceptions;

/// <summary>
/// Плохой запрос
/// </summary>
public class BadRequestException : ApplicationBaseException
{
    /// <inheritdoc />
    public BadRequestException(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }
}