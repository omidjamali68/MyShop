using MyShop.Application.Interfaces;
using MyShop.Common.Dto;
using MyShop.Domain.Aggregates.Shops;

namespace MyShop.Application.Services.Shops.Commands.Delete
{
    public class DeleteShopService : IDeleteShopService
    {
        private readonly IApplicationDbContext _context;

        public DeleteShopService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResultDto> Execute(int shopId)
        {
            var result = ServiceResultDto.Create();
            var shop = await _context.Shops.FindAsync(shopId);

            GuardAgainstShopNotFound(shop, result);

            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync();

            return result;
        }

        private void GuardAgainstShopNotFound(Shop? shop, ServiceResultDto result)
        {
            if (shop is null)
            {
                result.IsSuccess = false;
                result.Message.Add("فروشگاه مورد نظر یافت نشد");
            }
        }
    }
}
