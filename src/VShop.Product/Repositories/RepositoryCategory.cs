using Context;
using Entities;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class RepositoryCategory(ApplicationDbContext context) : RepositoryBase<Category>(context), IRepositoryCategory
{
    public async Task<Category?> GetByName(string name)
    => await _context.Categories.FirstOrDefaultAsync( c => c.Name == name );

    public async Task<IEnumerable<Category>> GetCategoriesProducts()
    => await _context.Categories.Include(e => e.Products).ToListAsync();        
}
