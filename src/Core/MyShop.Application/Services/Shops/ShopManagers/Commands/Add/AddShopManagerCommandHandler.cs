﻿using MyShop.Application.Interfaces;
using MyShop.Application.Services.Managers;
using MyShop.Common;
using MyShop.Common.Dto;
using MyShop.Domain;
using MyShop.Domain.SeedWork;
using MyShop.Domain.SharedKernel;

namespace MyShop.Application.Services.Shops.ShopManagers.Commands.Add
{
    public sealed class AddShopManagerCommandHandler : ICommandHandler<AddShopMangerCommand>
    {        
        private readonly IShopRepository _shopRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddShopManagerCommandHandler(
            IShopRepository shopRepository,
            IUnitOfWork unitOfWork,
            IManagerRepository managerRepository)
        {
            _shopRepository = shopRepository;
            _unitOfWork = unitOfWork;
            _managerRepository = managerRepository;
        }

        public async Task<Result> Handle(AddShopMangerCommand request, CancellationToken cancellationToken)
        {
            var shop = await _shopRepository.FindById(request.ShopId);
            
            if (shop is null)
            {
                return Result.Failure(Error.Create("ShopManger.Add.NotFound",
                    string.Format(Common.Messages.Validations.NotExist, DataDictionary.ShopName)));

            }

            var mobile = MobileNumber.Create(request.MobileNumber);

            if (mobile.IsFailure)
            {
                return Result.Failure<ServiceResultDto>(mobile.Error);
            }

            var selectedManager = await _managerRepository.FindByMobileNumber(mobile.Value);

            if (selectedManager is null)
                shop.Value.AssignNewManager(request.FirstName, request.LastName, request.Age, request.MobileNumber);
            else
                shop.Value.AssignExistManager(selectedManager);

            if (shop.IsFailure)
            {
                return Result.Failure(shop.Error);
            }

            await _unitOfWork.SaveChangeAsync();
            
            return Result.Success();
        }
        
    }
}
