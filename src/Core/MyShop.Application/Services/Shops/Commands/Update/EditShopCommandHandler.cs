using MyShop.Application.Interfaces;
using MyShop.Common;
using MyShop.Common.Messages;
using MyShop.Domain;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Services.Shops.Commands.Update
{
    public sealed class EditShopCommandHandler : ICommandHandler<EditShopCommand>
    {
        private readonly IShopRepository _shopRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EditShopCommandHandler(IShopRepository shopRepository, IUnitOfWork unitOfWork)
        {
            _shopRepository = shopRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(EditShopCommand request, CancellationToken cancellationToken)
        {
            var shop = await _shopRepository.FindById(request.ShopId);

            if (shop is null)
            {
                return Result.Failure(
                    Error.Create(
                        "Shop.Edit.NotExist",
                        string.Format(Validations.NotExist, DataDictionary.Shop)));
            }

            var result = shop.Value!.Update(request.Name, request.Address);

            if (result.IsFailure)
            {
                return result;
            }

            await _unitOfWork.SaveChangeAsync();

            return result;
        }
    }
}
