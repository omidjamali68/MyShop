namespace MyShop.Application.Infra
{
    public abstract class RequestGetListDto
    {
        public string? SearchKey { get; set; }
        public int Page { get; set; }
    }
}
