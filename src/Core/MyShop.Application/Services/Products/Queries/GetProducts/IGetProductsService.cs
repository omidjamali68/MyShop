using MyShop.Domain.Core.RepositoryContracts;

namespace MyShop.Application.Services.Products.Queries.GetProducts
{
    public interface IGetProductsService : IService
    {
        Task<ResultGetProducts> Execute(RequestGetProductsDto dto);
    }
}
