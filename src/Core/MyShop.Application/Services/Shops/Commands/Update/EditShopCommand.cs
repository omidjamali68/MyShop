using MyShop.Application.Interfaces;

namespace MyShop.Application.Services.Shops.Commands.Update
{
    public sealed record EditShopCommand(int ShopId, string Name, string Address) : ICommand;
}
