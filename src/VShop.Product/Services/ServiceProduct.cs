using AutoMapper;
using DTOs;
using Entities;
using Interfaces;

namespace Services;

public class ServiceProduct(IRepositoryBase<Product> repository, IMapper mapper) : ServiceBase<Product, ProductDTO>(repository, mapper), IServiceProduct
{
}
