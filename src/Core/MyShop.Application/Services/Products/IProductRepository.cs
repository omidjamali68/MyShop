using MyShop.Application.Services.Products.Queries.GetProducts;
using MyShop.Domain.Aggregates.Products;
using MyShop.Domain.Core.RepositoryContracts;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Services.Products
{
    public interface IProductRepository : IRepository
    {
        Task Add(Product product);
        void Delete(Product product);
        Task<Product> FindById(int productId);        
        Task<Result<GetProductsResponse>> GetProducts(GetProductsCommand request);
    }
}
