using Microsoft.EntityFrameworkCore;
using MyShop.Application.Services.Products.Categories;
using MyShop.Application.Services.Products.Categories.Queries;
using MyShop.Common;

namespace MyShop.Persistence.Products
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetCategoriesResponse> GetAll(GetCategoriesQuery request)
        {
            var categories = _context.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                categories = categories.Where(
                    x => ((string)x.Name).ToLower().Contains(request.SearchKey.ToLower()));
            }

            var categoriesList = categories.Select(x => 
                new GetCategoriesDto(x.Id, x.Name.Value));

            int rowsCount = 0;
            var result = await categoriesList
                .ToPaged(request.Page, 20, out rowsCount)
                .ToListAsync();

            return new GetCategoriesResponse
            {
                Rows = rowsCount,
                Data = result
            };
        }
    }
}
