using FluentValidation;
using ProductsMicroService.BusinessLogicLayer.DTO;

namespace ProductsMicroService.BusinessLogicLayer.Validators;
public sealed class ProductAddRequestValidator : AbstractValidator<ProductAddRequest>
{
    public ProductAddRequestValidator()
    {
        RuleFor(productAddRequest => productAddRequest.ProductName)
            .NotEmpty().WithMessage("Product name is required.");
        RuleFor(productAddRequest => productAddRequest.ProductCategory)
            .IsInEnum().WithMessage("Product category is required.");
        RuleFor(productAddRequest => productAddRequest.UnitPrice)
            .GreaterThan(0).WithMessage("Product price must be greater than zero.");
        RuleFor(productAddRequest => productAddRequest.QuantityInStock)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity in stock cannot be negative.");
    }
}
