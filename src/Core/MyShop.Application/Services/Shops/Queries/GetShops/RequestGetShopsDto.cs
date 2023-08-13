using MyShop.Application.Infra;

namespace MyShop.Application.Services.Shops.Queries.GetShops
{
    public sealed record RequestGetShopsDto : RequestGetListDto
    {
        public RequestGetShopsDto(string? SearchKey, int Page) : base(SearchKey, Page)
        {
        }
    }
}
