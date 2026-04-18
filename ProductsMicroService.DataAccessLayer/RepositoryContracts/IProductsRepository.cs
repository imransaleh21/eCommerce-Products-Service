using ProductsMicroService.DataAccessLayer.Entity;
using System.Linq.Expressions;

namespace ProductsMicroService.DataAccessLayer.RepositoryContracts;
/// <summary>
/// This interface defines the contract for a products repository,
/// which provides methods for performing CRUD (Create, Read, Update, Delete) operations on Product entities.
/// It includes methods to retrieve products based on specific conditions,
/// as well as methods to add, update, and delete products from the data source.
/// </summary>
public interface IProductsRepository
{
    /// <summary>
    /// Asynchronously retrieves a collection of available products.
    /// </summary>
    /// <returns>The task result contains an enumerable collection of product</returns>
    Task<IEnumerable<Product?>> GetProducts();
    /// <summary>
    /// Asynchronously retrieves a collection of products that satisfy the specified condition.
    /// </summary>
    /// <param name="conditionExpression">An expression that defines the condition used to filter products.</param>
    /// <returns>The task result contains an enumerable collection of products
    /// that match the specified condition, or null if no products are found.</returns>
    Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression);
    /// <summary>
    /// Asynchronously retrieves the first product that matches the specified condition.
    /// </summary>
    /// <param name="conditionExpression">An expression that defines the condition to filter products.</param>
    /// <returns>The task result contains the first matching product if found; otherwise, null.</returns>
    Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression);
    /// <summary>
    /// Adds a new product to the data store asynchronously.
    /// </summary>
    /// <param name="product">The product to add. Cannot be null.</param>
    /// <returns>The task result contains the added product</returns>
    Task<Product> AddProduct(Product product);
    /// <summary>
    ///Updates the specified product with new values and returns the updated product.
    /// </summary>
    /// <param name="product">The product to update. Must contain the updated values and a valid identifier.</param>
    /// <returns>The task result contains the updated product, or null if the product does not exist.</returns>
    Task<Product?> UpdateProduct(Product product);
    /// <summary>
    /// Deletes the product with the specified identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to delete.</param>
    /// <returns> The task result contains true if the product was deleted; otherwise, false.</returns>
    Task<bool> DeleteProduct(Guid productId);
}
