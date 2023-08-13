using MediatR;
using MyShop.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyShop.Application.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, IMediator
    {
        //private readonly IEnumerable<IValidator<TRequest>> _validators;

        //public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        //{
        //    _validators = validators;
        //}

        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            //if (!_validators.Any())
            //{
            //    return await next();
            //}

            //var context = new ValidationContext<TRequest>(request);

            throw new NotImplementedException();
        }
    }
}
