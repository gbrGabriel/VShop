namespace VShopProduct.Models;

public class ProductInputModel
{
    /// <summary>
    /// Name for new product
    /// </summary>
    /// <example>Nome do produto</example>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Price of product
    /// </summary>
    /// <example>1.50</example>
    public decimal Price { get; set; }

    /// <summary>
    /// Description of product
    /// </summary>
    /// <example>Descrição do produto</example>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Stock of product
    /// </summary>
    /// <example>10</example>
    public long Stock { get; set; }

    /// <summary>
    /// URL image of product
    /// </summary>
    /// <example>produto.png</example>
    public string ImageUrl { get; set; } = null!;

    /// <summary>
    /// Id of category product
    /// </summary>
    /// <example>1</example>
    public int CategoryId { get; set; }

}
