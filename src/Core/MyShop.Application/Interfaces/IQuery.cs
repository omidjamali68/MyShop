using MediatR;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Interfaces
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
