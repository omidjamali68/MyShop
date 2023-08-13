using MyShop.Application.Services.Products.Categories.Queries;
using MyShop.Domain.Core.RepositoryContracts;

namespace MyShop.Application.Services.Products.Categories
{
    public interface ICategoryRepository : IRepository
    {
        Task<GetCategoriesResponse> GetAll(GetCategoriesQuery request);
    }
}
