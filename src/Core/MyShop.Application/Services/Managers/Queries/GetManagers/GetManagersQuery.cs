using MyShop.Application.Interfaces;

namespace MyShop.Application.Services.Managers.Queries.GetManagers
{
    public sealed record GetManagersQuery(string? searchKey, int page) : IQuery<GetManagersResponse>;
}
