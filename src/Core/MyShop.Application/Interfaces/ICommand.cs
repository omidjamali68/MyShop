using MediatR;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Interfaces
{
    public interface ICommand : IRequest<Result>
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
