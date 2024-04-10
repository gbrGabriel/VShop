using AutoMapper;
using DTOs;
using Entities;
using Interfaces;

namespace Services;

public class ServiceProduct(IRepositoryProduct repository, IMapper mapper) : ServiceBase<Product, ProductDTO>(repository, mapper), IServiceProduct
{

}
