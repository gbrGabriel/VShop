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
        try
        {
            ViewBag.CategoryId = new
                      SelectList(await _serviceCategory.GetAllAsync("/api/v1/category"), "Id", "Name");
            return View();
        }
        catch (Exception)
        {
            return View("Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> UpdateProduct(int id)
    {
        try
        {
            ViewBag.CategoryId = new
                        SelectList(await _serviceCategory.GetAllAsync("/api/v1/category"), "Id", "Name");

            var result = await _serviceProduct.GetByIdAsync("/api/v1/product", id);

            if (result == null)
                return View("Error");

            return View(result);
        }
        catch (Exception)
        {
            return View("Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            var result = await _serviceProduct.GetByIdAsync("/api/v1/product", id);

            if (result == null)
                return View("Error");

            return View(result);
        }
        catch (Exception)
        {
            return View("Error");
        }
    }

    [HttpPost, ActionName("DeleteProduct")]
    public async Task<IActionResult> DeleteProductById(int id)
    {
        try
        {
            var result = await _serviceProduct.DeleteAsync($"/api/v1/product/{id}");

            if (!result)
                return View("Error");

            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            return View("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct(ProductViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var result = await _serviceProduct.PutAsync($"/api/v1/product/{model.Id}", model);

                if (result != null)
                    return RedirectToAction("Index");
            }
            return View(model);
        }
        catch (Exception)
        {
            return View("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductViewModel model)
    {
        try
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
        catch (Exception)
        {
            return View("Error");
        }
    }
}
