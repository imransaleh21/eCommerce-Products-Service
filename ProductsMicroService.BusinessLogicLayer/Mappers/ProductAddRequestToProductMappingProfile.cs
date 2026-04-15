using AutoMapper;
using ProductsMicroService.BusinessLogicLayer.DTO;
using ProductsMicroService.DataAccessLayer.Entity;

namespace ProductsMicroService.BusinessLogicLayer.Mappers;
public class ProductAddRequestToProductMappingProfile : Profile
{
    /// <summary>
    ///Configuring mappings from ProductAddRequest to Product objects.
    /// </summary>
    /// <remarks>This mapping profile is intended for use with AutoMapper to transform data from a
    /// ProductAddRequest DTO to a Product entity. The ProductID property is ignored during mapping, while other
    /// properties are mapped directly or converted as needed. 
    /// </remarks>
    public ProductAddRequestToProductMappingProfile()
    {
        CreateMap<ProductAddRequest, Product>()
            .ForMember(dest => dest.ProductID, opt => opt.Ignore())
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.ProductCategory.ToString()))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock));
    }
}
