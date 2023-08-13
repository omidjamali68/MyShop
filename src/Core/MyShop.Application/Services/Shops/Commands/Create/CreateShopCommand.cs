using MyShop.Application.Interfaces;

namespace MyShop.Application.Services.Shops.Commands.Create;

public sealed record CreateShopCommand(RegisterShopDto dto) : ICommand<int>;

