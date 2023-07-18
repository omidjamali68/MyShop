using MyShop.Common.Dto;
using MyShop.Domain.Core.RepositoryContracts;

namespace MyShop.Application.Services.Shops.Commands.Update
{
    public interface IChageShopStatusService : IService
    {
        Task<ServiceResultDto> Execute(int shopId);
    }
}
