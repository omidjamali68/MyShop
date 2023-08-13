using Microsoft.EntityFrameworkCore;
using MyShop.Application.Services.Managers;
using MyShop.Application.Services.Managers.Queries.GetManager;
using MyShop.Application.Services.Managers.Queries.GetManagers;
using MyShop.Common;
using MyShop.Domain.Aggregates.Managers;
using MyShop.Domain.SeedWork;
using MyShop.Domain.SharedKernel;

namespace MyShop.Persistence.Managers
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly ApplicationDbContext _context;

        public ManagerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Delete(Manager manager)
        {
            _context.Managers.Remove(manager);
        }

        public async Task<Manager?> FindById(int id)
        {
            return await _context.Managers.FindAsync(id);
        }

        public async Task<Manager?> FindByMobileNumber(MobileNumber value)
        {
            return await _context.Managers.FirstOrDefaultAsync(x => x.MobileNumber == value);
        }

        public async Task<Result<GetManagersResponse>> GetAll(string? searchKey, int page)
        {            
            var managers = _context.Managers.AsQueryable();            

            if (!string.IsNullOrEmpty(searchKey))
                managers = managers.Where(x => x.FirstName.Contains(searchKey) ||
                    x.LastName.Contains(searchKey) ||
                    x.ShopeManagers.Any(_ => ((string)_.Shop.Name).Contains(searchKey))
                    );

            var selectedManager = managers.Select(x => new GetManagersDto
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
                    Name = (string)sh.Shop.Name
                }).ToList()
            });

            int rowsCount = 0;
            var shopList = await selectedManager
                .ToPaged(page, 20, out rowsCount)
                .ToListAsync();

            return new GetManagersResponse
            {
                Rows = rowsCount,
                Data = shopList,
            };
        }

        public async Task<Result<GetManagerResponse>> GetById(int id)
        {
            var manager = await _context.Managers.FindAsync(id);

            if (manager is null)
                return new GetManagerResponse();

            return new GetManagerResponse
            {
                FirstName = manager.FirstName,
                LastName = manager.LastName,
                Age = manager.Age.Value,
                MobileNumber = manager.MobileNumber.Value,
            };
        }
    }
}
