using System.ComponentModel.DataAnnotations;

namespace VShopProduct.DTOs;

public class CategoryDTO
{
    /// <summary>
    /// Id of category
    /// </summary>
    /// <example>1</example>
    public int? Id { get; set; }

    /// <summary>
    /// Name for new category
    /// </summary>
    /// <example>Nome da categoria</example>
    [Required(ErrorMessage = "The {0} is required")]
    [StringLength(
        maximumLength: 255,
        MinimumLength = 3,
        ErrorMessage = "The field must be between {1} and {0}"
    )]
    public string Name { get; set; } = null!;
    public ICollection<ProductDTO>? Products { get; set; }
}
