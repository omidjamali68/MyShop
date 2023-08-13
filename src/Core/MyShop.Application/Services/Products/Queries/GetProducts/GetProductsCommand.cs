using MyShop.Application.Interfaces;

namespace MyShop.Application.Services.Products.Queries.GetProducts
{
    public sealed record GetProductsCommand(string? SearchKey, int Page) : IQuery<GetProductsResponse>;    
}