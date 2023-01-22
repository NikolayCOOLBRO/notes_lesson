using FluentValidation;
using MediatR;

namespace Notes.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> m_Validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) 
            => m_Validators = validators;

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = m_Validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(failures => failures != null)
                .ToList();
            if(failures.Count != 0)
            {
                throw new ValidationException(failures);
            }
            return next();
        }
    }
}
