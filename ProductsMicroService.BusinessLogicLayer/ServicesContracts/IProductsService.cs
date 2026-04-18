using ProductsMicroService.BusinessLogicLayer.DTO;
using ProductsMicroService.DataAccessLayer.Entity;
using System.Linq.Expressions;

namespace ProductsMicroService.BusinessLogicLayer.ServicesContracts;
public interface IProductsService
{
    /// <summary>
    /// Asynchronously retrieves a list of products.
    /// </summary>
    /// <returns> List of <see cref="ProductResponse"/> objects representing the products.</returns>
    Task<List<ProductResponse?>> GetProducts();
    /// <summary>
    /// Asynchronously retrieves a list of products that match the specified condition.
    /// </summary>
    /// <param name="predicate">A function to check each product for a condition.</param>
    /// <returns>The task result contains a list of <see cref="ProductResponse"/> objects representing the products that match the condition.</returns>
    Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> predicate);
    /// <summary>
    /// Asynchronously retrieves a single product that matches the specified condition.
    /// </summary>
    /// <param name="predicate">A function to check the product for a condition.</param>
    /// <returns>The task result contains a <see cref="ProductResponse"/> object representing the product that matches the condition, or null if no match is found.</returns>
    Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> predicate);
    /// <summary>
    /// Asynchronously adds a new product with the specified details.
    /// </summary>
    /// <param name="productAddRequest">The request object containing the details of the product to add.</param>
    /// <returns>The task result contains a <see cref="ProductResponse"/> object representing the added product, or null if the addition failed.</returns>
    Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest);
    /// <summary>
    /// Updates an existing product with the specified details.
    /// </summary>
    /// <param name="productUpdateRequest">An object containing the updated product information.</param>
    /// <returns>The task result contains a ProductResponse object with the
    /// updated product details if the update is successful; otherwise, null.</returns>
    Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest);
    /// <summary>
    /// Asynchronously deletes a product with the specified ID.
    /// </summary>
    /// <param name="productId">The ID of the product to delete.</param>
    /// <returns>The task result contains true if the deletion is successful; otherwise, false.</returns>
    Task<bool> DeleteProduct(Guid productId);
}
