using MyShop.Application.Interfaces;
using MyShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Application.Services.Products.Queries.GetProducts
{
    public sealed class GetProductsCommandHandler : IQueryHandler<GetProductsCommand, GetProductsResponse>
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
