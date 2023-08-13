using Microsoft.AspNetCore.Http;
using MyShop.Application.Interfaces;

namespace MyShop.Application.Services.Products.Commands.Add
{
    public sealed record AddProductCommand(
        string Name,
        string Brand,
        string Description,
        int Price,
        int Quantity,
        int CategoryId,
        bool Displayed,
        List<IFormFile> Images,
        List<AddNewProduct_FeaturesDto> Features) : ICommand;

    public sealed record AddProductDto(
        string Name,
        string Brand,
        string Description,
        int Price,
        int Quantity,
        int CategoryId,
        bool Displayed,
        List<IFormFile> Images,
        List<AddNewProduct_FeaturesDto> Features);
}
