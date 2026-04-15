using AutoMapper;
using ProductsMicroService.BusinessLogicLayer.DTO;
using ProductsMicroService.BusinessLogicLayer.Enum;
using ProductsMicroService.DataAccessLayer.Entity;

namespace ProductsMicroService.BusinessLogicLayer.Mappers;
public class ProductToProductResponseMappingProfile : Profile
{
    /// <summary>
    /// Configuring the mapping between Product and ProductResponse types.
    /// </summary>
    /// <remarks>This mapping profile defines how properties from a Product instance are mapped to a
    /// ProductResponse instance
    /// </remarks>
    public ProductToProductResponseMappingProfile()
    {
        CreateMap<Product, ProductResponse>()
            .ConstructUsing(src => new ProductResponse(
                src.ProductID,
                src.ProductName,
                System.Enum.Parse<ProductsCategoryOption>(src.Category),
                src.UnitPrice,
                src.QuantityInStock
            ));
    }
}
