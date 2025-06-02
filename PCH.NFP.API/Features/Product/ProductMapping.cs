using AutoMapper;
using PCH.NFP.API.Entities;
using PCH.NFP.Shared.Models;

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