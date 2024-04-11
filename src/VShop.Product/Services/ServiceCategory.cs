using AutoMapper;
using VShopProduct.DTOs;
using VShopProduct.Entities;
using VShopProduct.Interfaces;

namespace VShopProduct.Services;

public class ServiceCategory(IRepositoryCategory repository, IMapper mapper) : ServiceBase<Category, CategoryDTO>(repository, mapper), IServiceCategory
{
    public async Task<IEnumerable<CategoryDTO>> GetCategoriesProducts()
        => MapListEntityToDto(await repository.GetCategoriesProducts());
}
