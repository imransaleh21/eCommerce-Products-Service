using Microsoft.EntityFrameworkCore;
using ProductsMicroService.DataAccessLayer.Entity;

namespace ProductsMicroService.DataAccessLayer.DBContext;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                ProductID = Guid.Parse("1a9df78b-3f46-4c3d-9f2a-1b9f69292a77"),
                ProductName = "Apple iPhone 15 Pro Max",
                Category = "Electronics",
                UnitPrice = 1299.99,
                QuantityInStock = 50
            },
            new Product
            {
                ProductID = Guid.Parse("2c8e8e7c-97a3-4b11-9a1b-4dbe681cfe17"),
                ProductName = "Samsung Foldable Smart Phone 2",
                Category = "Electronics",
                UnitPrice = 1499.99,
                QuantityInStock = 100
            },
            new Product
            {
                ProductID = Guid.Parse("3f3e8b3a-4a50-4cd0-8d8e-1e178ae2cfc1"),
                ProductName = "Ergonomic Office Chair",
                Category = "Furniture",
                UnitPrice = 249.99,
                QuantityInStock = 25
            },
            new Product
            {
                ProductID = Guid.Parse("4c9b6f71-6c5d-485f-8db2-58011a236b63"),
                ProductName = "Coffee Table with Storage",
                Category = "Furniture",
                UnitPrice = 179.99,
                QuantityInStock = 30
            },
            new Product
            {
                ProductID = Guid.Parse("5d7e36bf-65c3-4a71-bf97-740d561d8b65"),
                ProductName = "Samsung QLED 75 inch",
                Category = "Electronics",
                UnitPrice = 1999.99,
                QuantityInStock = 20
            },
            new Product
            {
                ProductID = Guid.Parse("6a14f510-72c1-42c8-9a5a-8ef8f3f45a0d"),
                ProductName = "Running Shoes",
                Category = "Furniture",
                UnitPrice = 49.99,
                QuantityInStock = 75
            },
            new Product
            {
                ProductID = Guid.Parse("7b39ef14-932b-4c84-9187-55b748d2b28f"),
                ProductName = "Anti-Theft Laptop Backpack",
                Category = "Accessories",
                UnitPrice = 59.99,
                QuantityInStock = 60
            },
            new Product
            {
                ProductID = Guid.Parse("8c5f6e73-68fc-49d9-99b4-aecc3706a4f4"),
                ProductName = "LG OLED 65 inch",
                Category = "Electronics",
                UnitPrice = 1499.99,
                QuantityInStock = 15
            },
            new Product
            {
                ProductID = Guid.Parse("9e7e7085-6f4e-4921-8f15-c59f084080f9"),
                ProductName = "Modern Dining Table",
                Category = "Furniture",
                UnitPrice = 699.99,
                QuantityInStock = 10
            },
            new Product
            {
                ProductID = Guid.Parse("10d7b110-ecdb-4921-85a4-58a5d1b32bf4"),
                ProductName = "PlayStation 5",
                Category = "Electronics",
                UnitPrice = 499.99,
                QuantityInStock = 40
            },
            new Product
            {
                ProductID = Guid.Parse("11f2e86a-9d5d-42f9-b3c2-3e4d652e3df8"),
                ProductName = "Executive Office Desk",
                Category = "Furniture",
                UnitPrice = 299.99,
                QuantityInStock = 18
            },
            new Product
            {
                ProductID = Guid.Parse("12b369b7-9101-41b1-a653-6c6c9a4fe1e4"),
                ProductName = "Breville Smart Blender",
                Category = "HomeAppliances",
                UnitPrice = 129.99,
                QuantityInStock = 50
            }
        );
    }

}
