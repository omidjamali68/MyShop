using MyShop.Application.Interfaces;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Services.Products.Queries.GetProducts
{
    internal sealed class GetProductsCommandHandler : IQueryHandler<GetProductsCommand, GetProductsResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<GetProductsResponse>> Handle(
            GetProductsCommand request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetProducts(request);
        }
    }
}
