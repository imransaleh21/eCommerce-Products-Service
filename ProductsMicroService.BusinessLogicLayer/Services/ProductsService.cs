using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using ProductsMicroService.BusinessLogicLayer.DTO;
using ProductsMicroService.BusinessLogicLayer.Mappers;
using ProductsMicroService.BusinessLogicLayer.ServicesRepository;
using ProductsMicroService.DataAccessLayer.Entity;
using ProductsMicroService.DataAccessLayer.RepositoryContracts;
using System.Linq.Expressions;

namespace ProductsMicroService.BusinessLogicLayer.Services;

public class ProductsService : IProductsService
{
    private readonly IProductsRepository _productsRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ProductUpdateRequest> _productUpdateRequestValidator;
    private readonly IValidator<ProductAddRequest> _productAddRequestValidator;
    public ProductsService(IProductsRepository productsRepository, IMapper mapper,
        IValidator<ProductAddRequest> productAddRequestValidator,
        IValidator<ProductUpdateRequest> productUpdateRequestValidator)
    {
        _productsRepository = productsRepository;
        _mapper = mapper;
        _productAddRequestValidator = productAddRequestValidator;
        _productUpdateRequestValidator = productUpdateRequestValidator;
    }
    public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
    {
        if (productAddRequest == null) throw new ArgumentNullException(nameof(productAddRequest));

        ValidationResult validationResult = await _productAddRequestValidator.ValidateAsync(productAddRequest);
        if (!validationResult.IsValid)
        {
            string errors = string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException(errors);
        }

        Product productEntity = _mapper.Map<Product>(productAddRequest);
        Product addedProduct = await _productsRepository.AddProduct(productEntity);
        return _mapper.Map<ProductResponse>(addedProduct);
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        if (productId == Guid.Empty) throw new ArgumentException("Invalid product ID.", nameof(productId));
        Product? existingProduct = await _productsRepository.GetProductByCondition(p => p.ProductID == productId);
        if (existingProduct is null) return false;
        return await _productsRepository.DeleteProduct(productId);
    }

    public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> predicate)
    {
        Product? existingProduct = await _productsRepository.GetProductByCondition(predicate);
        if (existingProduct is null) return null;
        return _mapper.Map<ProductResponse>(existingProduct);
    }

    public async Task<List<ProductResponse?>?> GetProducts()
    {
        IEnumerable<Product?> productEntities = await _productsRepository.GetProducts();
        if (productEntities is null || !productEntities.Any()) return null;
        return _mapper.Map<IEnumerable<ProductResponse?>>(productEntities).ToList();
    }

    public async Task<List<ProductResponse?>?> GetProductsByCondition(Expression<Func<Product, bool>> predicate)
    {
        IEnumerable<Product?> existingProducts = await _productsRepository.GetProductsByCondition(predicate);
        if (existingProducts is null || !existingProducts.Any()) return null;
        return _mapper.Map<IEnumerable<ProductResponse?>>(existingProducts).ToList();
    }

    public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {
        if (productUpdateRequest == null) throw new ArgumentNullException(nameof(productUpdateRequest));

        ValidationResult validationResult = await _productUpdateRequestValidator.ValidateAsync(productUpdateRequest);
        if (!validationResult.IsValid)
        {
            string errors = string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException(errors);
        }
        Product? existingProduct = await _productsRepository.GetProductByCondition(p => p.ProductID == productUpdateRequest.ProductID);
        if(existingProduct is null) throw new ArgumentException("Invalid Product to Update");

        Product productEntity = _mapper.Map<Product>(productUpdateRequest);
        Product? updatedProduct = await _productsRepository.UpdateProduct(productEntity);
        return _mapper.Map<ProductResponse>(updatedProduct);
    }
}