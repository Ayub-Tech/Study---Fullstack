using FluentValidation;
using MediatR;

namespace Study.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var failures = (await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, ct))))
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .ToList();

        if (failures.Count == 0)
            return await next();

        var dict = failures
            .GroupBy(f => f.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(f => f.ErrorMessage).ToArray());

        var respType = typeof(TResponse);
        var failMethod = respType.GetMethod(
            "Fail",
            new[] { typeof(string), typeof(string), typeof(Dictionary<string, string[]>) }
        );

        if (failMethod is not null)
        {
            return (TResponse)failMethod.Invoke(
                null,
                new object[] { "VALIDATION_ERROR", "Validation failed.", dict }
            )!;
        }

        throw new InvalidOperationException("ValidationBehavior: TResponse must expose static Fail(code, message, validationErrors).");
    }
}
