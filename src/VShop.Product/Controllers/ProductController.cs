using Microsoft.AspNetCore.Mvc;
using VShopProduct.DTOs;
using VShopProduct.Interfaces;

namespace VShopProduct.Controllers;

[Route("api/v1/product")]
public class ProductController(IServiceProduct serviceProduct) : AbstractApiBaseController
{
    private readonly IServiceProduct _serviceProduct = serviceProduct;

    /// <summary>
    /// Obtém todos os produtos cadastrados no sistema.
    /// </summary>
    /// <returns>Uma coleção de produtos.</returns>
    /// <response code="200">Retorna uma coleção de produtos</response>
    [HttpGet()]
    [ProducesResponseType(typeof(IEnumerable<ProductDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll() => Ok(await _serviceProduct.GetAllAsync());

    /// <summary>
    /// Obtém um produto cadastrado no sistema por id.
    /// </summary>
    /// <param name="id">Id do produto cadastrada no sistema.</param>
    /// <returns>Um produto cadastrado.</returns>
    /// <response code="200">Retorna o produto encontrado.</response>
    /// <response code="404">Mensagem de não encontrado.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var product = await _serviceProduct.GetByIdAsync(id);

        if (product == null)
            return NotFound($"No register for id.");

        return Ok(product);
    }
}
