using MyShop.Application.Interfaces;

namespace MyShop.Application.Services.Managers.Queries.GetManager
{
    public sealed record GetManagerQuery(int ManagerId) : IQuery<GetManagerResponse>;
}
