using System.ComponentModel.DataAnnotations;
using Entities;

namespace DTOs;

public class CategoryDTO
{
    [Required(ErrorMessage = "The {0} is required")]
    [StringLength(
        maximumLength: 255,
        MinimumLength = 3,
        ErrorMessage = "The field must be between {1} and {0}"
    )]
    public string Name { get; set; } = null!;
    public ICollection<Product>? Products { get; set; }
}
