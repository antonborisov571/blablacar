using System.Net;

namespace Blablacar.Core.Exceptions.AuthExceptions;

/// <summary>
/// Исключение выбрасывается, если пользователь пытается зарегистрироваться с почтой, которая уже есть в бд
/// </summary>
public class EmailAlreadyRegisteredException : ApplicationBaseException
{
    /// <inheritdoc />
    public EmailAlreadyRegisteredException(string message,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message, statusCode)
    {
    }

    /// <inheritdoc />
    public EmailAlreadyRegisteredException() 
    {
    }
}