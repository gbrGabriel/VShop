using Context;
using Entities;
using Interfaces;

namespace Repositories;

public class RepositoryProduct(ApplicationDbContext context) : RepositoryBase<Product>(context), IRepositoryProduct
{
}
