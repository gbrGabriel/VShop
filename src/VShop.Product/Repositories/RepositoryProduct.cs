using VShopProduct.Context;
using VShopProduct.Entities;
using VShopProduct.Interfaces;

namespace VShopProduct.Repositories;

public class RepositoryProduct(ApplicationDbContext context) : RepositoryBase<Product>(context), IRepositoryProduct
{
}
