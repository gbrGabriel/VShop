using Microsoft.AspNetCore.Mvc;
using VShopProduct.DTOs;
using VShopProduct.Interfaces;

namespace VShopProduct.Controllers;

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
    [ProducesResponseType(typeof(IEnumerable<CategoryDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCategories() => Ok(await _serviceCategory.GetAllAsync());

    /// <summary>
    /// Obtém todos os produtos de todas categorias cadastrada no sistema.
    /// </summary>
    /// <returns>Uma coleção de categorias.</returns>
    /// <response code="200">Retorna uma coleção de categorias</response>
    [HttpGet()]
    [ProducesResponseType(typeof(IEnumerable<CategoryDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllProductCategories() => Ok(await _serviceCategory.GetCategoriesProducts());

    /// <summary>
    /// Obtém uma categorias cadastrada no sistema por id.
    /// </summary>
    /// <param name="id">Id da categoria cadastrada no sistema.</param>
    /// <returns>Uma categoria cadastrado.</returns>
    /// <response code="200">Retorna a categoria encontrado.</response>
    /// <response code="404">Mensagem de não encontrado.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _serviceCategory.GetByIdAsync(id);

        if (category == null)
            return NotFound($"No register for id.");

        return Ok(category);
    }

    /// <summary>
    /// Deleta uma categoria por id.
    /// </summary>
    /// <param name="id">Id da categoria cadastrada no sistema.</param>
    /// <returns>StatusCode 204</returns>
    ///  <remarks>
    /// Atenção!
    /// Caso delete uma categoria, a exclusão será em cascade para as demais entidades que dependem de categoria.
    /// </remarks>
    /// <response code="204">Retorna StatusCode 204.</response>
    /// <response code="404">Mensagem de não encontrado.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _serviceCategory.GetByIdAsync(id);

        if (category == null)
            return NotFound($"No register for id.");

        await _serviceCategory.DeleteAsync(category);

        return NoContent();
    }
}
