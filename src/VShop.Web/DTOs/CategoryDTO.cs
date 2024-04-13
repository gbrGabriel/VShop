namespace VShopWeb.DTOs;

public class CategoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<ProductDTO>? Products { get; set; }
}
