using MyShop.Application.Interfaces;

namespace MyShop.Application.Services.Shops.Commands.Create;

public sealed record CreateShopCommand(string Name, string Address) : ICommand<int>;

