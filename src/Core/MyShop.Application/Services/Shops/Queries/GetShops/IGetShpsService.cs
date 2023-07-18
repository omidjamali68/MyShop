using MyShop.Domain.Core.RepositoryContracts;

namespace MyShop.Application.Services.Shops.Queries.GetShops
{
    public interface IGetShpsService : IService
    {
        Task<ResultGetShopsDto> Execute(RequestGetShopsDto request);
    }
}
