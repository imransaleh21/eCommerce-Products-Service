using Microsoft.EntityFrameworkCore;
using ProductsMicroService.DataAccessLayer.DBContext;
using ProductsMicroService.DataAccessLayer.Entity;
using ProductsMicroService.DataAccessLayer.RepositoryContracts;
using System.Linq.Expressions;

namespace ProductsMicroService.DataAccessLayer.Repository;

public class ProductsRepository : IProductsRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ProductsRepository(ApplicationDbContext applicationDbContext)
    {
        _dbContext = applicationDbContext;
    }
    public async Task<Product> AddProduct(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }
    public async Task<bool> DeleteProduct(Guid productId)
    {
        Product? product = await _dbContext.Products.FindAsync(productId);
        if (product == null) return false;
        
        _dbContext.Products.Remove(product);
        int affectedRows = await _dbContext.SaveChangesAsync();
        return affectedRows > 0;
    }

    public async Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        Product? product = await _dbContext.Products.FirstOrDefaultAsync(conditionExpression);
        if (product == null) return null;
        return product;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product>?> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        return await _dbContext.Products.Where(conditionExpression).ToListAsync();
    }

    public async Task<Product?> UpdateProduct(Product product)
    {
        Product? existingProduct = await _dbContext.Products.FindAsync(product.ProductID);
        if (existingProduct == null) return null;

        _dbContext.Entry(existingProduct).CurrentValues.SetValues(product);    
        await _dbContext.SaveChangesAsync();
        return product;
    }
}
