using MyShop.Application.Interfaces;
using MyShop.Domain;
using MyShop.Domain.Aggregates.Shops;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Services.Shops.Commands.Create
{
    internal sealed class CreateShopCommandHandler : ICommandHandler<CreateShopCommand, int>
    {
        private readonly IShopRepository _shopRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateShopCommandHandler(IShopRepository shopRepository, IUnitOfWork unitOfWork)
        {
            _shopRepository = shopRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateShopCommand request, CancellationToken cancellationToken)
        {
            var shop = Shop.Create(request.Name, request.Address, true);

            if (shop.IsFailure)
            {
                return Result.Failure<int>(shop.Error);
            }

            await _shopRepository.Insert(shop.Value);
            await _unitOfWork.SaveChangeAsync();

            return Result.Success(shop.Value.Id);
        }
        
    }
}
