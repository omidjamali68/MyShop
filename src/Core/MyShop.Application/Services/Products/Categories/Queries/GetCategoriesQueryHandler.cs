using MyShop.Application.Interfaces;
using MyShop.Domain.SeedWork;

namespace MyShop.Application.Services.Products.Categories.Queries
{
    internal sealed class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, GetCategoriesResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<GetCategoriesResponse>> Handle(
            GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetAll(request);            
        }
    }
}
