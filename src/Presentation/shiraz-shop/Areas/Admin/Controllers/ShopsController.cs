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
        private readonly IGetShpsService _getShopsService;
        private readonly IRegisterShopService _registerShopService;
        private readonly IDeleteShopService _deleteShopService;
        private readonly IChageShopStatusService _chageShopStatusService;
        private readonly IEditShopService _editShopService;
        private readonly IGetManagersService _getManagersService;

        public ShopsController(
            IGetShpsService getUsersService,
            IRegisterShopService registerShopService,
            IDeleteShopService deleteShopService,
            IChageShopStatusService chageShopStatusService,
            IEditShopService editShopService,
            IGetManagersService getManagersService)
        {
            _getShopsService = getUsersService;
            _registerShopService = registerShopService;
            _deleteShopService = deleteShopService;
            _chageShopStatusService = chageShopStatusService;
            _editShopService = editShopService;
            _getManagersService = getManagersService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchKey, int page = 1)
        {
            var model = await _getShopsService.Execute(new RequestGetShopsDto
            {
                Page = page,
                SearchKey = searchKey
            });

            var managers = await _getManagersService.Execute(new RequestGetManagersDto
            {
                Page = 1,
                SearchKey = null
            });

            ViewBag.ExistManagers = new SelectList(managers.Data, "Id", "FullName");

            return View(model);
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

            var res = await _registerShopService.Execute(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int shopId)
        {
            var result = await _deleteShopService.Execute(shopId);
            return Json(result);
        }

        [HttpPatch]
        public async Task<IActionResult> ShopSatusChange(int shopId)
        {
            var result = await _chageShopStatusService.Execute(shopId);
            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditShopDto dto)
        {
            var result = await _editShopService.Execute(dto);
            return Json(result);
        }
    }
}
