using MyShop.Application.Interfaces;

namespace MyShop.Application.Services.Products.Commands.Delete
{
    public sealed record DeleteProductCommand(int ProductId) : ICommand;    
}
