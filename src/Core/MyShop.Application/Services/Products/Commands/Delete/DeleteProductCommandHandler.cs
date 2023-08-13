using MyShop.Application.Interfaces;
using MyShop.Common;
using MyShop.Common.Messages;
using MyShop.Domain;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Services.Products.Commands.Delete
{
    internal sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindById(request.ProductId);

            if (product == null)
            {
                return Result.Failure(
                    Error.Create(
                        "Product.Delete.NotFound",
                        string.Format(Validations.NotExist, DataDictionary.Product)));
            }

            _productRepository.Delete(product);

            await _unitOfWork.SaveChangeAsync();

            return Result.Success();
        }
    }
}
