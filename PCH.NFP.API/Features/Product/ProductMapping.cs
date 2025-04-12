using AutoMapper;
using PCH.NFP.API.Models;
using PCH.NFP.API.Entities;

namespace PCH.NFP.API.Features;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<Entities.Product, ProductDto>().ReverseMap();
        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();
    }
}