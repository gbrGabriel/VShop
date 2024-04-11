using Microsoft.EntityFrameworkCore;
using VShopProduct.Context;
using VShopProduct.Entities;
using VShopProduct.Interfaces;

namespace VShopProduct.Repositories;

public class RepositoryCategory(ApplicationDbContext context) : RepositoryBase<Category>(context), IRepositoryCategory
{
    public async Task<Category?> GetByName(string name)
    => await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);

    public async Task<IEnumerable<Category>> GetCategoriesProducts()
    => await _context.Categories.AsNoTracking().Include(e => e.Products).ToListAsync();
}
