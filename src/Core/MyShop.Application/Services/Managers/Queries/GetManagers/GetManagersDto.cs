namespace MyShop.Application.Services.Managers.Queries.GetManagers
{
    public sealed record GetManagersDto
    {
        public GetManagersDto()
        {
            Shops = new List<ShopManagersDto>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public byte Age { get; set; }
        public string Mobile { get; set; }
        public List<ShopManagersDto> Shops { get; set; }
    }
}