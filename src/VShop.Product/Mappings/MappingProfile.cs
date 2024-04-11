using AutoMapper;
using VShopProduct.DTOs;
using VShopProduct.Entities;

namespace VShopProduct.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();

        CreateMap<ProductDTO, Product>().ReverseMap();

        CreateMap<Product, ProductDTO>().ForMember(e => e.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
    }
}
