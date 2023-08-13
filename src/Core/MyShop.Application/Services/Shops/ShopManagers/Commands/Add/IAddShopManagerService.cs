using MyShop.Common.Dto;
using MyShop.Domain.Core.RepositoryContracts;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Services.Shops.ShopManagers.Commands.Add
{
    public interface IAddShopManagerService : IService
    {
        Task<Result<ServiceResultDto>> Execute(AddShopManagerDto dto);
    }
}
