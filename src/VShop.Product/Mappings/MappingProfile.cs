using AutoMapper;
using VShopProduct.DTOs;
using VShopProduct.Entities;

namespace VShopProduct.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
    }
}
