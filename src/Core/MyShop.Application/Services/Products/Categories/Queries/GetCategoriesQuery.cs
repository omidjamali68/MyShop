using MyShop.Application.Interfaces;

namespace MyShop.Application.Services.Products.Categories.Queries
{
    public sealed record GetCategoriesQuery(string? SearchKey, int Page) : IQuery<GetCategoriesResponse>;    
}
