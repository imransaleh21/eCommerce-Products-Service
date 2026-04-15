using ProductsMicroService.BusinessLogicLayer.Enum;

namespace ProductsMicroService.BusinessLogicLayer.DTO;
public record ProductResponse(
    Guid ProductID,
    string ProductName,
    ProductsCategoryOption ProductCategory,
    double? UnitPrice,
    int? QuantityInStock
    );
