namespace MyShop.Application.Services.Shops.ShopManagers.Commands.Add
{
    public class AddShopManagerDto
    {
        public int ShopId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte Age { get; set; }
        public string MobileNumber { get; set; }
    }
}
