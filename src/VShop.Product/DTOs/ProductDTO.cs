using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VShopProduct.DTOs;

public class ProductDTO
{
    /// <summary>
    /// Id of product
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }

    /// <summary>
    /// Name for new product
    /// </summary>
    /// <example>Nome do produto</example>
    [Required(ErrorMessage = "The {0} is required")]
    [StringLength(
        maximumLength: 255,
        MinimumLength = 3,
        ErrorMessage = "The field must be between {1} and {0}"
    )]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Price of product
    /// </summary>
    /// <example>1.50</example>
    [Required(ErrorMessage = "The {0} is required")]
    [Range(1, 999999)]
    public decimal Price { get; set; }

    /// <summary>
    /// Description of product
    /// </summary>
    /// <example>Descrição do produto</example>
    [Required(ErrorMessage = "The {0} is required")]
    [StringLength(
        maximumLength: 255,
        MinimumLength = 3,
        ErrorMessage = "The field must be between {1} and {0}"
    )]
    public string Description { get; set; } = null!;

    /// <summary>
    /// Stock of product
    /// </summary>
    /// <example>10</example>
    [Required(ErrorMessage = "The {0} is required")]
    public long Stock { get; set; }

    /// <summary>
    /// URL image of product
    /// </summary>
    /// <example>produto.png</example>
    [Required(ErrorMessage = "The {0} is required")]
    [StringLength(
        maximumLength: 255,
        MinimumLength = 3,
        ErrorMessage = "The field must be between {1} and {0}"
    )]
    public string ImageUrl { get; set; } = null!;

    [JsonIgnore]
    public string? CategoryName { get; set; }

    /// <summary>
    /// Id of category product
    /// </summary>
    /// <example>1</example>
    [Required(ErrorMessage = "The {0} is required")]
    [DisplayName("Category")]
    public int CategoryId { get; set; }
}
