using MyShop.Application.Interfaces;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Services.Products.Categories.Queries
{
    public sealed class GetGategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, GetCategoriesResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetGategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<GetCategoriesResponse>> Handle(
            GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var products = await _categoryRepository.GetAll(request);

            return products;
        }
    }
}
