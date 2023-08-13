using MediatR;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Interfaces
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
