using MyShop.Application.Infra;

namespace MyShop.Application.Services.Managers.Queries.GetManagers
{
    public sealed record RequestGetManagersDto : RequestGetListDto
    {
        public RequestGetManagersDto(string? SearchKey, int Page) : base(SearchKey, Page)
        {
        }
    }
}