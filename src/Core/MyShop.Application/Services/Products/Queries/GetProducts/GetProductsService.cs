using Microsoft.EntityFrameworkCore;
using MyShop.Application.Interfaces;
using MyShop.Common;

namespace MyShop.Application.Services.Products.Queries.GetProducts
{
    public class GetProductsService : IGetProductsService
    {
        private readonly IApplicationDbContext _context;

        public GetProductsService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultGetProducts> Execute(RequestGetProductsDto request)
        {
            var products = _context.Products.Select(x => new GetProductsDto { 
                Id = x.Id,
                Name = x.Name.Value,
                Quantity = x.Quantity.Value
            });

            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                products = products.Where(p => p.Name.Contains(request.SearchKey));
            }

            int rowsCount = 0;
            var productsList = await products
                .ToPaged(request.Page, 20, out rowsCount)
                .ToListAsync();

            return new ResultGetProducts { Data = productsList, Rows = rowsCount };
        }
    }
}
