using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Services.Shops.ShopManagers.Commands.Add;

namespace shiraz_shop.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ShopManagersController : Controller
    {
        private readonly IMediator _mediator;

        public ShopManagersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddShopMangerCommand dto)
        {
            var result = await _mediator.Send(dto);
            return Json(result);
        }

    }
}
