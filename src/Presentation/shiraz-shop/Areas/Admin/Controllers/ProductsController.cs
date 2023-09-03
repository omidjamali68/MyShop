using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.Application.Services.Products.Categories.Queries;
using MyShop.Application.Services.Products.Commands.Add;
using MyShop.Application.Services.Products.Commands.Delete;
using MyShop.Application.Services.Products.Queries.GetProducts;

namespace shiraz_shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchKey, int page = 1)
        {
            var model = await _mediator.Send(new GetProductsCommand(searchKey, page));
            
            return View(model.Value);
        }         

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _mediator.Send(new GetCategoriesQuery(null,1));
            ViewBag.Categories = new SelectList(categories.Value.Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            AddProductDto request, List<AddNewProduct_FeaturesDto> Features)
        {
            List<IFormFile> images = new List<IFormFile>();

            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }

            var command = new AddProductCommand(
                request.Name, 
                request.Brand, 
                request.Description, 
                request.Price, 
                request.Quantity, 
                request.CategoryId, 
                request.Displayed, 
                images, 
                Features);

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteProductCommand request)
        {
            return Json(await _mediator.Send(request));
        }
    }
}
