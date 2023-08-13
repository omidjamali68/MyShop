using MyShop.Application.Interfaces;
using MyShop.Domain;
using MyShop.Domain.Aggregates.Products;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Services.Products.Commands.Add
{
    public sealed class AddProductCommandHandler : ICommandHandler<AddProductCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AddProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create(
                request.Name, 
                request.Quantity, 
                request.Brand, 
                request.Description, 
                request.Price, 
                request.Displayed, 
                request.CategoryId);

            if (product.IsFailure)
            {
                return Result.Failure(product.Error);
            }

            await _repository.Add(product.Value);
            await _unitOfWork.SaveChangeAsync();

            return Result.Success();
        }
    }
}
