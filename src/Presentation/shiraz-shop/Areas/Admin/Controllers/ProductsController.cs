using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Services.Products.Queries.GetProducts;

namespace shiraz_shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IGetProductsService _getProductsService;

        public ProductsController(IGetProductsService getProductsService)
        {
            _getProductsService = getProductsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchKey, int page = 1)
        {
            var model = await _getProductsService.Execute(new RequestGetProductsDto
            {
                SearchKey = searchKey,
                Page = page,
            });

            return View(model);
        }
        
        public IActionResult Create()
        {
            return View();
        }
    }
}
