using System.Net;
using FluentValidation;
using Blablacar.Core.Exceptions;
using Newtonsoft.Json;

namespace Blablacar.WEB.Middlewares;

/// <summary>
/// Middleware, отвечающий за обработку ошибок
/// </summary>
public class ExceptionMiddleware(ILoggerFactory loggerFactory)
    : IMiddleware
{
    /// <inheritdoc cref="IMiddleware"/>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ApplicationBaseException exception)
        {
            context.Response.StatusCode = (int)exception.ResponseStatusCode;

            var logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
            logger.LogWarning("Внимание: {Message}", exception.Message);

            await context.Response.WriteAsJsonAsync(new { message = exception.Message });
        }
        catch (ValidationException exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            
            var logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
            logger.LogWarning("Ошибка валидации: {Message}", exception.Message);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(exception.Errors);
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            
            var logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
            logger.LogWarning("Ошибка на сервере: {Message}", exception.Message);

            await context.Response.WriteAsJsonAsync(new { message = exception.Message });
        }
    }
}
