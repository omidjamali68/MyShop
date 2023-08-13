using Microsoft.EntityFrameworkCore;
using MyShop.Application.Services.Products;
using MyShop.Application.Services.Products.Categories.Queries;
using MyShop.Application.Services.Products.Queries.GetProducts;
using MyShop.Common;
using MyShop.Domain.Aggregates.Products;
using MyShop.Domain.SeedWork;

namespace MyShop.Persistence.Products
{
    public sealed class ProductRepoository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepoository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<Product?> FindById(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }        

        public async Task<Result<GetProductsResponse>> GetProducts(GetProductsCommand request)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                products = products.Where(p => ((string)p.Name).Contains(request.SearchKey) ||
                    p.Brand.Contains(request.SearchKey) ||
                    ((string)p.Category.Name).Contains(request.SearchKey));
            }

            var productResults = products.Select(x => new GetProductsDto
            {
                Id = x.Id,
                Name = x.Name.Value,
                Quantity = x.Quantity.Value,
                Brand = x.Brand,
                Displayed = x.Displayed,
                Price = x.Price,
                Category = x.Category.Name.Value
            });

            int rowsCount = 0;
            var productsList = await productResults
                .ToPaged(request.Page, 20, out rowsCount)
                .ToListAsync();

            return new GetProductsResponse
            {
                Rows = rowsCount,
                Data = productsList
            };
        }
    }
}
