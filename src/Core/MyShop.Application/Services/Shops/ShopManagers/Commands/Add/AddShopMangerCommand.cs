using MyShop.Application.Interfaces;

namespace MyShop.Application.Services.Shops.ShopManagers.Commands.Add
{
    public sealed record AddShopMangerCommand(
        int ShopId, string FirstName, string LastName, byte Age, string MobileNumber) : ICommand;    
}
