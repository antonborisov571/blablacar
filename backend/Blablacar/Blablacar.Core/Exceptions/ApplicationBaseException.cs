using System.Net;

namespace Blablacar.Core.Exceptions;

/// <summary>
/// Базовый класс ошибок
/// </summary>
public class ApplicationBaseException : Exception
{
    /// <summary>
    /// Статус код
    /// </summary>
    public HttpStatusCode ResponseStatusCode { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public ApplicationBaseException()
    {
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="message">Сообщение об ошибку</param>
    public ApplicationBaseException(string message)
    {
        
    }
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="message">Сообщение об ошибке</param>
    /// <param name="statusCode">Код ошибки</param>
    public ApplicationBaseException(string message, HttpStatusCode statusCode)
        : base(message)
    {
        ResponseStatusCode = statusCode;
    }
}