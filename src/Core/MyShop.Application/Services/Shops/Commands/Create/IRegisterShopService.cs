using MyShop.Common.Dto;
using MyShop.Domain.Core.RepositoryContracts;

namespace MyShop.Application.Services.Shops.Commands.Create
{
    public interface IRegisterShopService : IService
    {
        Task<ServiceResultDto> Execute(RegisterShopDto dto);
    }
}
