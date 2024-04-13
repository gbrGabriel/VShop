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

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        CreateMap<Product, ProductDTO>().ForMember(e => e.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
}
