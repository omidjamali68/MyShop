using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Services.Managers.Commands.Delete;
using MyShop.Application.Services.Managers.Commands.Update;
using MyShop.Application.Services.Managers.Queries.GetManager;
using MyShop.Application.Services.Managers.Queries.GetManagers;

namespace shiraz_shop.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ManagersController : Controller
    {        
        private readonly IMediator _mediator;

        public ManagersController(IMediator mediator)
        {            
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(string? searchKey, int page = 1)
        {
            var model = await _mediator.Send(new GetManagersQuery(searchKey, page));
            return View(model.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditManagerCommand dto)
        {
            var result = await _mediator.Send(dto);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(GetManagerQuery dto)
        {
            var result = await _mediator.Send(dto);
            return Json(result.Value);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteManagerCommand dto)
        {
            var result = await _mediator.Send(dto);
            return Json(result);
        }
    }
}
