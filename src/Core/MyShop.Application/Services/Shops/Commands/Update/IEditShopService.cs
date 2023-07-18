using MyShop.Common.Dto;
using MyShop.Domain.Core.RepositoryContracts;

namespace MyShop.Application.Services.Shops.Commands.Update
{
    public interface IEditShopService : IService
    {
        Task<ServiceResultDto> Execute(EditShopDto dto);
    }
}
