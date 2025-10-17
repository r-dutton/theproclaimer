using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Sample.App.Pipelines;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = new List<ValidationFailure>();

            foreach (var validator in _validators)
            {
                var result = await validator.ValidateAsync(context, cancellationToken);
                if (!result.IsValid)
                {
                    failures.AddRange(result.Errors);
                }
            }

            if (failures.Count > 0)
            {
                _logger.LogWarning("Validation pipeline caught {Count} issues for {Request}", failures.Count, typeof(TRequest).Name);
                throw new ValidationException(failures);
            }
        }

        return await next();
    }
}
