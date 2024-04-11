using Microsoft.AspNetCore.Mvc;
using VShopProduct.DTOs;
using VShopProduct.Interfaces;
using VShopProduct.Models;

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
        try
        {
            var category = await _serviceCategory.GetByIdAsync(id);

            if (category == null)
                return NotFound($"No register for id.");

            return Ok(category);
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Grava uma nova categoria no sistema.
    /// </summary>
    /// <param name="model">Dados da categoria a ser gravado.</param>
    /// <returns>A nova categoria cadastrado.</returns>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST 
    ///     {
    ///        "name": "Categoria teste"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Retorna a nova categoria cadastrado.</response>
    /// <response code="400">Retorna uma mensagem de erro.</response>
    [HttpPost()]
    [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecordCategory([FromBody] CategoryInputModel model)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(model.Name))
                return BadRequest("Name is null or empty");

            return Ok(await _serviceCategory.AddSaveAsync(new CategoryDTO { Id = 0, Name = model.Name }));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while record the category.");
        }
    }

    /// <summary>
    /// Atualiza uma categoria por id.
    /// </summary>
    /// <param name="id">Id da categoria cadastrada no sistema.</param>
    /// <param name="name">Novo nome para categoria cadastrada no sistema.</param>
    /// <returns>Categoria atualizada no sistema</returns>
    /// <response code="200">Retorna a categoria atualizada.</response>
    /// <response code="400">Mensagem de erro nos parametros.</response>
    /// <response code="404">Mensagem de não encontrado.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCategory(int id, [FromQuery] string name)
    {
        try
        {
            if (id <= 0)
                return BadRequest("The ID cannot be equal to or less than zero");

            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name is null or empty");

            var category = await _serviceCategory.GetByIdAsync(id);

            if (category == null)
                return NotFound($"No register for id.");

            category.Name = name;

            await _serviceCategory.UpdateAsync(category);

            return Ok(category);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the category.");
        }
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
        try
        {
            var category = await _serviceCategory.GetByIdAsync(id);

            if (category == null)
                return NotFound($"No register for id.");

            await _serviceCategory.DeleteAsync(category);

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while delete the category.");

        }
    }
}
