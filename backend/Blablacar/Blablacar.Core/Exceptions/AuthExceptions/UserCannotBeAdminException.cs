using System.Net;

namespace Blablacar.Core.Exceptions.AuthExceptions;

/// <summary>
/// User не админ
/// </summary>
public class UserCannotBeAdminException : ApplicationBaseException
{
    /// <inheritdoc />
    public UserCannotBeAdminException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode)
    {
        
    }

    /// <inheritdoc />
    public UserCannotBeAdminException()
    {
    }
}