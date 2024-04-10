using DTOs;
using Entities;

namespace Interfaces;

public interface IServiceCategory : IServiceBase<Category, CategoryDTO>
{
    Task<IEnumerable<Category>> GetCategoriesProducts();
}
