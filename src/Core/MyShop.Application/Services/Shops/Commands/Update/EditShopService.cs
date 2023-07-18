using MyShop.Application.Interfaces;
using MyShop.Common;
using MyShop.Common.Dto;
using MyShop.Common.Messages;

namespace MyShop.Application.Services.Shops.Commands.Update
{
    public class EditShopService : IEditShopService
    {
        private readonly IApplicationDbContext _context;

        public EditShopService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResultDto> Execute(EditShopDto dto)
        {
            var result = ServiceResultDto.Create();

            var shop = await _context.Shops.FindAsync(dto.ShopId);

            shop?.Update(dto.Name, dto.Address);  
            if (!shop.Result.IsSucces)
            {
                result.SetErrors(shop.Result.Messeges);
                return result;
            }

            await _context.SaveChangesAsync();

            result.SuccessFully(
                string.Format(Notifications.SuccessfullyUpdated, DataDictionary.Shop));

            return result;
        }
    }
}
