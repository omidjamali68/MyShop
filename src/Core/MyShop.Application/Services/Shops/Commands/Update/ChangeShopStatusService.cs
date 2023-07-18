using MyShop.Application.Interfaces;
using MyShop.Common.Dto;
using MyShop.Domain.Aggregates.Shops;

namespace MyShop.Application.Services.Shops.Commands.Update
{
    public class ChangeShopStatusService : IChageShopStatusService
    {
        private readonly IApplicationDbContext _context;

        public ChangeShopStatusService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResultDto> Execute(int shopId)
        {
            var result = ServiceResultDto.Create();
            var shop = await _context.Shops.FindAsync(shopId);

            GuardAgainstShopNotFound(shop, result);

            shop.ChangeStatus();

            await _context.SaveChangesAsync();

            if (result.IsSuccess)
                result.Message.Add("تغییر وضعیت با موفقیت انجام شد");

            return result;
        }        

        private void GuardAgainstShopNotFound(Shop? shop, ServiceResultDto result)
        {
            if (shop is null)
            {
                result.IsSuccess = false;
                result.Message.Add("فروشگاه یافت نشد");
            }
        }
    }
}
