namespace MyShop.Application.Services.Shops.Queries.GetShops
{
    public sealed record GetShopsDto(int Id, string Name, string Address, bool IsActive);
}
