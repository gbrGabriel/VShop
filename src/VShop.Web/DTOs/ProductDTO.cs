namespace VShopWeb.DTOs;

public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public long Stock { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public int CategoryId { get; set; }
}
