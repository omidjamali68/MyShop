using MyShop.Application.Interfaces;
using MyShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            shop.Update(dto.Name, dto.Address);            

            await _context.SaveChangesAsync();

            if (result.IsSuccess)
                result.Message.Add("ویرایش با موفقیت انجام شد");

            return result;
        }
    }
}
