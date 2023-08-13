using MyShop.Application.Interfaces;

namespace MyShop.Application.Services.Shops.Queries.GetShops;

public sealed record GetShopsQuery(string? SearchKey, int Page) : IQuery<GetShopsResponse>;


