using AutoMapper;
using VShopProduct.DTOs;
using VShopProduct.Entities;
using VShopProduct.Interfaces;

namespace VShopProduct.Services;

public class ServiceProduct(IRepositoryProduct repository, IMapper mapper) : ServiceBase<Product, ProductDTO>(repository, mapper), IServiceProduct
{

}
