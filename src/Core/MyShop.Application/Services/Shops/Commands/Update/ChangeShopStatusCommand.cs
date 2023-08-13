using MyShop.Application.Interfaces;

namespace MyShop.Application.Services.Shops.Commands.Update
{
    public sealed record ChangeShopStatusCommand(int Id) : ICommand;
}
