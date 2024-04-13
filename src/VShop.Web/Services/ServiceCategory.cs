using AutoMapper;
using VShopWeb.Interfaces;
using VShopWeb.Models;

namespace VShopWeb.Services;

public class ServiceCategory(IHttpClientFactory httpClientFactory, IMapper mapper) : ServiceBase<CategoryViewModel>(httpClientFactory, mapper), IServiceCategory
{
}
