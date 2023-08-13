using MyShop.Application.Interfaces;
using MyShop.Common;
using MyShop.Common.Messages;
using MyShop.Domain;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Services.Shops.Commands.Update
{
    public sealed class ChangeShopStatusCommandHandler : ICommandHandler<ChangeShopStatusCommand>
    {
        private readonly IShopRepository _shopRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ChangeShopStatusCommandHandler(IShopRepository shopRepository, IUnitOfWork unitOfWork)
        {
            _shopRepository = shopRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ChangeShopStatusCommand request, CancellationToken cancellationToken)
        {
            var shop = await _shopRepository.FindById(request.Id);

            if (shop == null) 
            {
                return Result.Failure(
                    Error.Create(
                        "Shop.ChangeStatus.ShopNotFound", 
                        string.Format(Validations.NotExist, DataDictionary.Shop)));
            }

            if (shop.IsFailure)
            {
                return Result.Failure(shop.Error);
            }

            var result = shop.Value.ChangeStatus();

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            await _unitOfWork.SaveChangeAsync();
            return Result.Success();
        }
    }
}
