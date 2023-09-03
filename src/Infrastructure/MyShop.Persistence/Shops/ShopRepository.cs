using Microsoft.EntityFrameworkCore;
using MyShop.Application.Services.Shops;
using MyShop.Application.Services.Shops.Queries.GetShops;
using MyShop.Common;
using MyShop.Domain.Aggregates.Shops;
using MyShop.Domain.SeedWork;

namespace MyShop.Persistence.Shops
{
    public sealed class ShopRepository : IShopRepository
    {
        private readonly ApplicationDbContext _context;

        public ShopRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Delete(Shop shop)
        {
            _context.Shops.Remove(shop);
        }

        public async Task<Shop?> FindById(int id)
        {
            return await _context.Shops.FindAsync(id);
        }

        public async Task<Result<GetShopsResponse>?> GetAll(string? searchKey, int page)
        {            
            var shops = _context.Shops.AsQueryable();            
            
            if (searchKey is not null)
                shops = shops
                    .Where(x => ((string)x.Name).ToLower().Contains(searchKey.ToLower()));                  
            
            var result = shops.Select(x =>
                    new GetShopsDto(x.Id, (string)x.Name, x.Address, x.IsActive));
            
            int rowsCount = 0;
            var shopList = await result
                .ToPaged(page, 20, out rowsCount)
                .ToListAsync();

            return Result.Success(new GetShopsResponse
            {
                Data = shopList,
                Rows = rowsCount
            });
        }

        public async Task Insert(Shop shop)
        {
            await _context.Shops.AddAsync(shop);
        }
    }
}
