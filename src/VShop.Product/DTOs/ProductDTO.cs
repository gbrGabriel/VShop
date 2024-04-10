using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DTOs;

public class ProductDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The {0} is required")]
    [StringLength(
        maximumLength: 255,
        MinimumLength = 3,
        ErrorMessage = "The field must be between {1} and {0}"
    )]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "The {0} is required")]
    [Range(1, 999999)]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "The {0} is required")]
    [StringLength(
        maximumLength: 255,
        MinimumLength = 3,
        ErrorMessage = "The field must be between {1} and {0}"
    )]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "The {0} is required")]
    public long Stock { get; set; }

    [Required(ErrorMessage = "The {0} is required")]
    [StringLength(
        maximumLength: 255,
        MinimumLength = 3,
        ErrorMessage = "The field must be between {1} and {0}"
    )]
    public string ImageUrl { get; set; } = null!;

    [Required(ErrorMessage = "The {0} is required")]
    [DisplayName("Category")]
    public int CategoryId { get; set; }
}
