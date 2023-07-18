using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Services.Managers.Commands.Update;
using MyShop.Application.Services.Managers.Queries.GetManager;
using MyShop.Application.Services.Managers.Queries.GetManagers;

namespace shiraz_shop.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ManagersController : Controller
    {
        private readonly IGetManagersService _getManagersService;
        private readonly IEditManagerService _editManagersService;
        private readonly IGetManagerService _getManagerService;

        public ManagersController(
            IGetManagersService getManagersService,
            IEditManagerService editManagersService,
            IGetManagerService getManagerService)
        {
            _getManagersService = getManagersService;
            _editManagersService = editManagersService;
            _getManagerService = getManagerService;
        }

        public async Task<IActionResult> Index(string? searchKey, int page = 1)
        {
            var model = await _getManagersService.Execute(
                new RequestGetManagersDto
                {
                    SearchKey = searchKey,
                    Page = page
                });
            return View(model);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditManagerDto dto)
        {
            var result = await _editManagersService.Execute(dto);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(GetManagerDto dto)
        {
            var result = await _getManagerService.Execute(dto);
            return Json(result);
        }
    }
}
