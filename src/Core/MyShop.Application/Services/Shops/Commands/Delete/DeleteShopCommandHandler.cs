using MyShop.Application.Interfaces;
using MyShop.Common;
using MyShop.Common.Messages;
using MyShop.Domain;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Services.Shops.Commands.Delete
{
    internal sealed class DeleteShopCommandHandler : ICommandHandler<DeleteShopCommand>
    {
        private readonly IShopRepository _shopRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteShopCommandHandler(IShopRepository shopRepository, IUnitOfWork unitOfWork)
        {
            _shopRepository = shopRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteShopCommand request, CancellationToken cancellationToken)
        {
            var shop = await _shopRepository.FindById(request.Id);

            if (shop == null)
            {
                return Result.Failure(Error.Create(
                    "Shop.Delete.NotFound",
                    string.Format(Validations.NotExist, DataDictionary.Shop)));
            }

            _shopRepository.Delete(shop.Value!);

            await _unitOfWork.SaveChangeAsync();

            return Result.Success();
        }
    }
}
