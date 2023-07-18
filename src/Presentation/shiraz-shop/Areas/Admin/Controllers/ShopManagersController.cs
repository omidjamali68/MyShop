using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Services.Shops.ShopManagers.Commands.Add;

namespace shiraz_shop.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ShopManagersController : Controller
    {
        private readonly IAddShopManagerService _addShopManagerService;

        public ShopManagersController(IAddShopManagerService addShopManagerService)
        {
            _addShopManagerService = addShopManagerService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddShopManagerDto dto)
        {
            var result = await _addShopManagerService.Execute(dto);
            return Json(result);
        }

    }
}
