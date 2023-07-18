using Microsoft.EntityFrameworkCore;
using MyShop.Application.Interfaces;
using MyShop.Common;

namespace MyShop.Application.Services.Managers.Queries.GetManagers
{
    public class GetManagersService : IGetManagersService
    {
        private readonly IApplicationDbContext _context;

        public GetManagersService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultGetManagersDto> Execute(RequestGetManagersDto dto)
        {
            var managers = _context.Managers.Select(x => new GetManagersDto
            {
                Age = x.Age.Value,
                FirstName = x.FirstName,
                LastName = x.LastName,
                FullName = x.FirstName + " " + x.LastName,
                Mobile = x.MobileNumber.Value,
                Id = x.Id,
                Shops = x.ShopeManagers.Select(sh => new ShopManagersDto
                {
                    ShopId = sh.ShopId,
                    Name = sh.Shop.Name
                }).ToList()
            });

            if (!string.IsNullOrEmpty(dto.SearchKey))
                managers = managers.Where(x => x.FirstName.Contains(dto.SearchKey) ||
                    x.LastName.Contains(dto.SearchKey) || 
                    x.Shops.Any(_ => _.Name.Contains(dto.SearchKey))
                    );

            int rowsCount = 0;
            var shopList = await managers
                .ToPaged(dto.Page, 20, out rowsCount)
                .ToListAsync();

            return new ResultGetManagersDto
            {
                Rows = rowsCount,
                Data = shopList,
            };
        }
    }
}
