using AutoMapper;
using VShopWeb.Interfaces;
using VShopWeb.Models;

namespace VShopWeb.Services;

public class ServiceProduct(IHttpClientFactory httpClientFactory, IMapper mapper) : ServiceBase<ProductViewModel>(httpClientFactory, mapper), IServiceProduct
{

}
