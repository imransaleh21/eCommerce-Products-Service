using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductsMicroService.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProductName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UnitPrice = table.Column<double>(type: "double", nullable: true),
                    QuantityInStock = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Category", "ProductName", "QuantityInStock", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("10d7b110-ecdb-4921-85a4-58a5d1b32bf4"), "Electronics", "PlayStation 5", 40, 499.99000000000001 },
                    { new Guid("11f2e86a-9d5d-42f9-b3c2-3e4d652e3df8"), "Furniture", "Executive Office Desk", 18, 299.99000000000001 },
                    { new Guid("12b369b7-9101-41b1-a653-6c6c9a4fe1e4"), "HomeAppliances", "Breville Smart Blender", 50, 129.99000000000001 },
                    { new Guid("1a9df78b-3f46-4c3d-9f2a-1b9f69292a77"), "Electronics", "Apple iPhone 15 Pro Max", 50, 1299.99 },
                    { new Guid("2c8e8e7c-97a3-4b11-9a1b-4dbe681cfe17"), "Electronics", "Samsung Foldable Smart Phone 2", 100, 1499.99 },
                    { new Guid("3f3e8b3a-4a50-4cd0-8d8e-1e178ae2cfc1"), "Furniture", "Ergonomic Office Chair", 25, 249.99000000000001 },
                    { new Guid("4c9b6f71-6c5d-485f-8db2-58011a236b63"), "Furniture", "Coffee Table with Storage", 30, 179.99000000000001 },
                    { new Guid("5d7e36bf-65c3-4a71-bf97-740d561d8b65"), "Electronics", "Samsung QLED 75 inch", 20, 1999.99 },
                    { new Guid("6a14f510-72c1-42c8-9a5a-8ef8f3f45a0d"), "Furniture", "Running Shoes", 75, 49.990000000000002 },
                    { new Guid("7b39ef14-932b-4c84-9187-55b748d2b28f"), "Accessories", "Anti-Theft Laptop Backpack", 60, 59.990000000000002 },
                    { new Guid("8c5f6e73-68fc-49d9-99b4-aecc3706a4f4"), "Electronics", "LG OLED 65 inch", 15, 1499.99 },
                    { new Guid("9e7e7085-6f4e-4921-8f15-c59f084080f9"), "Furniture", "Modern Dining Table", 10, 699.99000000000001 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
