using MyShop.Application.Interfaces;
using MyShop.Common;
using MyShop.Common.Dto;
using MyShop.Common.Messages;
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

        public async Task<ServiceResultDto> Execute(RegisterShopDto dto)
        {
            var result = ServiceResultDto.Create();
            var shop = Shop.Create(dto.Name, dto.Address, true);            
            if (!shop.Result.IsSucces)
            {
                result.SetErrors(shop.Result.Messeges);
                return result;
            }
            await _context.Shops.AddAsync(shop);
            await _context.SaveChangesAsync();

            result.SuccessFully(
                string.Format(Notifications.SuccessfullyAdded, DataDictionary.Shop));
            return result;
        }
    }
}
