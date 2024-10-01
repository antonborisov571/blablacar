using FluentValidation;
using MediatR;

namespace Blablacar.Core.Common.Behaviors;

/// <summary>
/// Подключение пайплайна валидации
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <inheritdoc cref="IPipelineBehavior{TRequest,TResponse}"/>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    /// <inheritdoc />
    public Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var failures = _validators
            .Select(validator => validator.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .ToList();

        return failures.Count == 0
            ? next()
            : throw new ValidationException(failures);
    }
}

/// <summary>
/// Подключение пайплайна валидации для IRequest
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ValidationBehaviorForRequest<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest: IRequest
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    
    /// <inheritdoc cref="IPipelineBehavior{TRequest,TResponse}"/>
    public ValidationBehaviorForRequest(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    /// <inheritdoc />
    public Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var failures = _validators
            .Select(validator => validator.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .ToList();

        return failures.Count == 0
            ? next()
            : throw new ValidationException(failures);
    }
}