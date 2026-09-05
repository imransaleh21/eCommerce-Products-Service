using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using ProductsMicroService.BusinessLogicLayer.DTO;
using ProductsMicroService.BusinessLogicLayer.RabbitMQ;
using ProductsMicroService.BusinessLogicLayer.ServicesContracts;
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
    private readonly IRabbitMQPublisher _rabbitMQPublisher;
    public ProductsService(IProductsRepository productsRepository, IMapper mapper,
        IValidator<ProductAddRequest> productAddRequestValidator,
        IValidator<ProductUpdateRequest> productUpdateRequestValidator,
        IRabbitMQPublisher rabbitMQPublisher)
    {
        _productsRepository = productsRepository;
        _mapper = mapper;
        _productAddRequestValidator = productAddRequestValidator;
        _productUpdateRequestValidator = productUpdateRequestValidator;
        _rabbitMQPublisher = rabbitMQPublisher;
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

    public async Task<List<ProductResponse?>> GetProducts()
    {
        IEnumerable<Product?> productEntities = await _productsRepository.GetProducts();
        return productEntities
            .Select(product => product == null ? null : _mapper.Map<ProductResponse>(product)).ToList();
    }

    public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> predicate)
    {
        IEnumerable<Product?> existingProducts = await _productsRepository.GetProductsByCondition(predicate);
        return existingProducts
            .Select(product => product is null ? null : _mapper.Map<ProductResponse>(product)).ToList();
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

        // Check if the product name is being updated (case-insensitive comparison)
        bool isProductNameUpdated = !string.Equals(existingProduct.ProductName, productUpdateRequest.ProductName, StringComparison.OrdinalIgnoreCase);

        Product productEntity = _mapper.Map<Product>(productUpdateRequest);
        Product? updatedProduct = await _productsRepository.UpdateProduct(productEntity);
        if(isProductNameUpdated)
        {
            string routeKey = "product.name.updated";
            ProductNameUpdateMsg productNameUpdateMessage = new ProductNameUpdateMsg(updatedProduct.ProductID, updatedProduct.ProductName);
            // Publish the updated product name to the message queue
            await _rabbitMQPublisher.PublishMessageAsync(routeKey, productNameUpdateMessage);
        }
        return _mapper.Map<ProductResponse>(updatedProduct);
    }
}