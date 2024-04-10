using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("api/v1/category")]
public class CategoryController(IServiceCategory serviceCategory) : AbstractApiBaseController
{
    private readonly IServiceCategory _serviceCategory = serviceCategory;

    /// <summary>
    /// Obtém todos as categorias cadastrados no sistema.
    /// </summary>
    /// <returns>Uma coleção de categorias.</returns>
    /// <response code="200">Retorna uma coleção de categorias</response>
    [HttpGet("products")]
    [ProducesResponseType(typeof(IEnumerable<Category>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCategories() => Ok(await _serviceCategory.GetAllAsync());

    /// <summary>
    /// Obtém todos os produtos de todas categorias cadastrada no sistema.
    /// </summary>
    /// <returns>Uma coleção de categorias.</returns>
    /// <response code="200">Retorna uma coleção de categorias</response>
    [HttpGet()]
    [ProducesResponseType(typeof(IEnumerable<Category>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllProductCategories() => Ok(await _serviceCategory.GetCategoriesProducts());

    /// <summary>
    /// Obtém uma categorias cadastrada no sistema por id.
    /// </summary>
    /// <param name="id">Id da categoria cadastrada no sistema.</param>
    /// <returns>Uma categoria cadastrado.</returns>
    /// <response code="200">Retorna a categoria encontrado.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _serviceCategory.GetByIdAsync(id);

        if (category == null)
            return NotFound($"No register for id.");

        return Ok(category);
    }
}
