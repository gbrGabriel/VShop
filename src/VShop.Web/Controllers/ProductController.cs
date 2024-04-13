using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShopWeb.Interfaces;
using VShopWeb.Models;

namespace VShopWeb.Controllers;

public class ProductController(IServiceProduct serviceProduct, IServiceCategory serviceCategory) : Controller
{
    private readonly IServiceProduct _serviceProduct = serviceProduct;
    private readonly IServiceCategory _serviceCategory = serviceCategory;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            return View(await _serviceProduct.GetAllAsync("/api/v1/product"));
        }
        catch (Exception)
        {
            return View("Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.CategoryId = new
                        SelectList(await _serviceCategory.GetAllAsync("/api/v1/category"), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _serviceProduct.PostAsync("/api/v1/product", model);

            if (result != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CategoryId = new
                       SelectList(await _serviceCategory.GetAllAsync("/api/v1/category"), "CategoryId", "Name");
            }
        }
        return View(model);
    }
}
