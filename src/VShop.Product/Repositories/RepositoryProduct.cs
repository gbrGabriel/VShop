using Microsoft.EntityFrameworkCore;
using VShopProduct.Context;
using VShopProduct.Entities;
using VShopProduct.Interfaces;

namespace VShopProduct.Repositories;

public class RepositoryProduct(ApplicationDbContext context) : RepositoryBase<Product>(context), IRepositoryProduct
{
    public override async Task<IEnumerable<Product>> GetAllAsync()
    => await _context.Products.Include(e => e.Category).ToListAsync();

    public override async Task<Product?> GetByIdAsync(int id)
   => await _context.Products.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);
}
