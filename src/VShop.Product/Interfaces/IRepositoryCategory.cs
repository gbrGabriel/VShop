using Entities;

namespace Interfaces;

public interface IRepositoryCategory : IRepositoryBase<Category>
{
    Task<IEnumerable<Category>> GetCategoriesProducts();
    Task<Category?> GetByName(string name);
}
