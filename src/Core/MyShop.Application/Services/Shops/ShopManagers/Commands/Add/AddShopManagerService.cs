﻿using Microsoft.EntityFrameworkCore;
using MyShop.Application.Interfaces;
using MyShop.Common.Dto;
using MyShop.Domain.Aggregates.Shops;
using MyShop.Domain.SeedWork;
using MyShop.Domain.SharedKernel;

namespace MyShop.Application.Services.Shops.ShopManagers.Commands.Add
{
    public class AddShopManagerService : IAddShopManagerService
    {
        private readonly IApplicationDbContext _context;

        public AddShopManagerService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResultDto> Execute(AddShopManagerDto dto)
        {
            var result = ServiceResultDto.Create();

            var shop = await _context.Shops.FindAsync(dto.ShopId);

            if (GuardAgainstShopNotExist(shop, result))
                return result;

            var mobile = MobileNumber.Create(dto.MobileNumber);

            if (!mobile.Result.IsSucces)
            {
                result.SetErrors(mobile.Result.Messeges);
                return result;
            }

            var selectedManager = await _context.Managers.FirstOrDefaultAsync(x => 
                x.MobileNumber == mobile);

            if (selectedManager is null)            
                shop.AssignNewManager(dto.FirstName, dto.LastName, dto.Age, dto.MobileNumber);            
            else
                shop.AssignExistManager(selectedManager);

            if (!shop.Result.IsSucces)
            {
                result.SetErrors(shop.Result.Messeges);
                return result;
            }

            await _context.SaveChangesAsync();

            result.Message.Add("مدیر با موفقیت اضافه شد");
            return result;
        }

        private static bool GuardAgainstShopNotExist(Shop? shop, ServiceResultDto result)
        {
            if (shop is null)
            {
                result.Message.Add(Common.Messages.Validations.ShopNotExist);
                result.IsSuccess = false;
                return true;
            }
            else
                return false;
        }
    }
}