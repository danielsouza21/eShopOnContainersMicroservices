using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ValidationException = Ordering.Application.Exceptions.ValidationException;

namespace Ordering.Application.Behaviours
{
    /// <summary>
    /// MediatR pipeline configuration (pre-processing behavior).
    /// </summary>
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators; //Represents all 'AbstractValidator' validators in the project (IValidator)

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Checks for any failures and throws a specific exception if an exception is found. For each request, 'FluentValidation' is used for validation.
        /// </summary>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                    throw new ValidationException(failures); //Used custom exception instead of 'FluentValidation'
            }

            return await next();
        }
    }
}
