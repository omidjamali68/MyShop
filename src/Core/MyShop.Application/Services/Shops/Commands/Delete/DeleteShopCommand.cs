using MyShop.Application.Interfaces;

namespace MyShop.Application.Services.Shops.Commands.Delete
{
    public sealed record DeleteShopCommand(int Id) : ICommand;   
}
