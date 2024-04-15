using Microsoft.AspNetCore.Mvc;
using VShopProduct.DTOs;
using VShopProduct.Interfaces;
using VShopProduct.Models;

namespace VShopProduct.Controllers;

[Route("api/v1/product")]
public class ProductController(IServiceProduct serviceProduct, IServiceCategory serviceCategory) : AbstractApiBaseController
{
    private readonly IServiceProduct _serviceProduct = serviceProduct;
    private readonly IServiceCategory _serviceCategory = serviceCategory;

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
    public async Task<IActionResult> GetProductById(int id)
    {
        try
        {
            var product = await _serviceProduct.GetByIdAsync(id);

            if (product == null)
                return NotFound($"No register for id.");

            return Ok(product);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while find the product.");
        }
    }

    /// <summary>
    /// Grava um novo produto no sistema.
    /// </summary>
    /// <param name="model">Dados do produto a ser gravado.</param>
    /// <returns>Um novo produto cadastrado.</returns>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST 
    ///     {
    ///        "Name": "Categoria teste",
    ///        "Price": 1.50,
    ///        "Description": "Descrição teste",
    ///        "Stock": 10,
    ///        "ImagemUrl": "produto.png",
    ///        "CategoryId": 1,
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Retorna o novo produto cadastrado.</response>
    /// <response code="400">Retorna uma mensagem de erro.</response>
    [HttpPost()]
    [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecordProduct([FromBody] ProductInputModel model)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(model.Name))
                return BadRequest("Name is null or empty");

            if (string.IsNullOrWhiteSpace(model.Description))
                return BadRequest("Description is null or empty");

            if (string.IsNullOrWhiteSpace(model.ImageUrl))
                return BadRequest("The url image is null or empty");

            if (model.Price <= 0)
                return BadRequest("The Price cannot be equal to or less than zero");

            if (model.Stock <= 0)
                return BadRequest("The Stock cannot be equal to or less than zero");

            if (model.CategoryId <= 0)
                return BadRequest("The id category cannot be equal to or less than zero");

            var category = await _serviceCategory.GetByIdAsync(model.CategoryId);
            if (category == null)
                return BadRequest("No categories found for the given ID");

            return Ok(await _serviceProduct.AddSaveAsync(new ProductDTO
            {
                Id = 0,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId
            }));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while record the product.");
        }
    }

    /// <summary>
    /// Atualiza um produto existente por id.
    /// </summary>
    /// <param name="id">Id do produto cadastrada no sistema.</param>
    /// <param name="model">Dados para atualizar o produto cadastrado no sistema.</param>
    /// <returns>Produto atualizado no sistema</returns>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST 
    ///     {
    ///        "Name": "Categoria teste",
    ///        "Price": 1.50,
    ///        "Description": "Descrição teste",
    ///        "Stock": 10,
    ///        "ImagemUrl": "produto.png",
    ///        "CategoryId": 1,
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Retorna o produto atualizada.</response>
    /// <response code="400">Mensagem de erro nos parametros.</response>
    /// <response code="404">Mensagem de não encontrado.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductInputModel model)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(model.Name))
                return BadRequest("Name is null or empty");

            if (string.IsNullOrWhiteSpace(model.Description))
                return BadRequest("Description is null or empty");

            if (string.IsNullOrWhiteSpace(model.ImageUrl))
                return BadRequest("The url image is null or empty");

            if (model.Price <= 0)
                return BadRequest("The Price cannot be equal to or less than zero");

            if (model.Stock <= 0)
                return BadRequest("The Stock cannot be equal to or less than zero");

            if (model.CategoryId <= 0)
                return BadRequest("The id category cannot be equal to or less than zero");

            var category = await _serviceCategory.GetByIdAsync(model.CategoryId);
            if (category == null)
                return BadRequest("No categories found for the given ID");

            var product = await _serviceProduct.GetByIdAsync(id);

            if (product == null)
                return BadRequest("No products found for the given ID");

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Stock = model.Stock;
            product.ImageUrl = model.ImageUrl;
            product.CategoryId = model.CategoryId;

            await _serviceProduct.AddUpdateAsync(product);

            return Ok(product);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the product.");
        }
    }

    /// <summary>
    /// Deleta um produto por id.
    /// </summary>
    /// <param name="id">Id do produto cadastrado no sistema.</param>
    /// <returns>StatusCode 204</returns>
    /// <response code="200">Retorna true ou false</response>
    /// <response code="404">Mensagem de não encontrado.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            var product = await _serviceProduct.GetByIdAsync(id);

            if (product == null)
                return NotFound($"No register for id.");

            return Ok(await _serviceProduct.DeleteAsync(product) > 0);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while delete the product.");

        }
    }
}
