using MyShop.Domain.Core.RepositoryContracts;

namespace MyShop.Application.Services.Shops.Commands.Create
{
    public interface IRegisterShopService : IService
    {
        Task<int> Execute(RegisterShopDto dto);
    }
}
