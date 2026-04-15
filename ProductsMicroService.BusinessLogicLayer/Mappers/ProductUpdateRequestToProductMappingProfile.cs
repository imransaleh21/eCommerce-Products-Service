using AutoMapper;
using ProductsMicroService.BusinessLogicLayer.DTO;
using ProductsMicroService.DataAccessLayer.Entity;

namespace ProductsMicroService.BusinessLogicLayer.Mappers;
public class ProductUpdateRequestToProductMappingProfile : Profile
{
    /// <summary>
    /// Configuring mappings from ProductUpdateRequest to Product.
    /// </summary>
    /// <remarks>This mapping profile defines how properties from a ProductUpdateRequest are mapped to a
    /// Product. Use this profile with
    /// </remarks>
    public ProductUpdateRequestToProductMappingProfile()
    {
        CreateMap<ProductUpdateRequest, Product>()
           .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
           .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
           .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.ProductCategory.ToString()))
           .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
           .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock));
    }
}
