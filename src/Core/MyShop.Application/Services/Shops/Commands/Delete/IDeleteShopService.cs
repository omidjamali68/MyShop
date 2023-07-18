using MyShop.Common.Dto;
using MyShop.Domain.Core.RepositoryContracts;

namespace MyShop.Application.Services.Shops.Commands.Delete
{
    public interface IDeleteShopService : IService
    {
        Task<ServiceResultDto> Execute(int shopId);
    }
}
