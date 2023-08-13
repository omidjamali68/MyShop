using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.Application.Services.Managers.Queries.GetManagers;
using MyShop.Application.Services.Shops.Commands.Create;
using MyShop.Application.Services.Shops.Commands.Delete;
using MyShop.Application.Services.Shops.Commands.Update;
using MyShop.Application.Services.Shops.Queries.GetShops;

namespace shiraz_shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShopsController : Controller
    {                
        private readonly IMediator _mediator;

        public ShopsController(IMediator mediator)
        {                        
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchKey, int page = 1)
        {            
            var model = await _mediator.Send(new GetShopsQuery(searchKey, page));

            var managers = await _mediator.Send(new GetManagersQuery(searchKey, page));

            ViewBag.ExistManagers = new SelectList(managers.Value.Data, "Id", "FullName");

            return View(model.Value);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterShopDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _mediator.Send(new CreateShopCommand(dto));
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int shopId)
        {
            var result = await _mediator.Send(new DeleteShopCommand(shopId));
            return Json(result);
        }

        [HttpPatch]
        public async Task<IActionResult> ShopSatusChange(int shopId)
        {
            var result = await _mediator.Send(new ChangeShopStatusCommand(shopId));
            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditShopDto dto)
        {
            var result = await _mediator.Send(new EditShopCommand(dto.ShopId, dto.Name, dto.Address));
            return Json(result);
        }
    }
}
