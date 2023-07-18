using MyShop.Application.Interfaces;
using MyShop.Domain.Aggregates.Shops;

namespace MyShop.Application.Services.Shops.Commands.Create
{
    public class RegisterShopService : IRegisterShopService
    {
        private readonly IApplicationDbContext _context;

        public RegisterShopService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Execute(RegisterShopDto dto)
        {
            var shop = Shop.Create(dto.Name, dto.Address, true);            

            await _context.Shops.AddAsync(shop);
            await _context.SaveChangesAsync();

            return shop.Id;
        }
    }
}
