using AutoMapper;
using DTOs;
using Entities;
using Interfaces;

namespace Services
{
    public class ServiceCategory(IRepositoryBase<Category> repository, IMapper mapper) : ServiceBase<Category, CategoryDTO>(repository, mapper), IServiceCategory
    {
      
    }
}
