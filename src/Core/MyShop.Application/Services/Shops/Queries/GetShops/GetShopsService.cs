using Microsoft.EntityFrameworkCore;
using MyShop.Application.Interfaces;
using MyShop.Common;

namespace MyShop.Application.Services.Shops.Queries.GetShops
{
    public class GetShopsService : IGetShpsService
    {
        private readonly IApplicationDbContext _context;

        public GetShopsService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultGetShopsDto> Execute(RequestGetShopsDto request)
        {            
            var shops = _context.Shops.Select(p => new GetShopsDto
            {
                Id = p.Id,
                Name = p.Name,
                Address = p.Address,
                IsActive = p.IsActive,
            });

            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                shops = shops.Where(p => p.Name.Contains(request.SearchKey) || 
                    p.Address.Contains(request.SearchKey));
            }

            int rowsCount = 0;
            var shopList = await shops
                .ToPaged(request.Page, 20, out rowsCount)
                .ToListAsync();

            return new ResultGetShopsDto
            {
                Rows = rowsCount,
                Data = shopList,
            };
        }
    }
}
