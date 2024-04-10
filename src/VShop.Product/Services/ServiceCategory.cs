using AutoMapper;
using DTOs;
using Entities;
using Interfaces;

namespace Services;

public class ServiceCategory(IRepositoryCategory repository, IMapper mapper) : ServiceBase<Category, CategoryDTO>(repository, mapper), IServiceCategory
{
    public async Task<IEnumerable<Category>> GetCategoriesProducts()
        => await repository.GetCategoriesProducts();
}
