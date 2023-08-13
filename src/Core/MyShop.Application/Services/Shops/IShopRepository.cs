using MyShop.Application.Services.Shops.Queries.GetShops;
using MyShop.Domain.Aggregates.Shops;
using MyShop.Domain.Core.RepositoryContracts;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Services.Shops
{
    public interface IShopRepository : IRepository
    {
        void Delete(Shop shop);
        Task<Result<Shop?>> FindById(int id);
        Task<Result<GetShopsResponse>> GetAll(string? searchKey, int page);
        Task Insert(Shop shop);
    }
}
