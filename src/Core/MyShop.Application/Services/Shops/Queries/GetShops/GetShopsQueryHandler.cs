using MyShop.Application.Interfaces;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Services.Shops.Queries.GetShops
{
    internal sealed class GetShopsQueryHandler : IQueryHandler<GetShopsQuery, GetShopsResponse>
    {
        private readonly IShopRepository _shopRepository;
        
        public GetShopsQueryHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<Result<GetShopsResponse>> Handle(
            GetShopsQuery query, CancellationToken cancellationToken)
        {
            return await _shopRepository.GetAll(query.SearchKey, query.Page);
        }
    }
}
