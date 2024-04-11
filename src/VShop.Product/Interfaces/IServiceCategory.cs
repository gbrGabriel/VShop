using VShopProduct.DTOs;
using VShopProduct.Entities;

namespace VShopProduct.Interfaces;

public interface IServiceCategory : IServiceBase<Category, CategoryDTO>
{
    Task<IEnumerable<CategoryDTO>> GetCategoriesProducts();
}
