using FluentValidation;
using FluentValidation.Results;
using ProductsMicroService.BusinessLogicLayer.DTO;
using ProductsMicroService.BusinessLogicLayer.ServicesContracts;

namespace ProductsMicroService.API.ApiEndpoints;

public static class ProductApiEndpoints
{
    /// <summary>
    /// Maps the product-related API endpoints to the specified endpoint route builder. This includes endpoints for
    /// retrieving, searching, adding, updating, and deleting products.
    /// </summary>
    /// <remarks>
    /// This method registers RESTful endpoints for product operations under the '/api/products'
    /// route prefix.
    /// </remarks>
    /// <param name="app">The endpoint route builder to which the product API endpoints will be mapped.</param>
    /// <returns>The same <see cref="IEndpointRouteBuilder"/> instance provided in <paramref name="app"/>, with product API
    /// endpoints configured.</returns>
    public static IEndpointRouteBuilder MapProductApiEndpoints(this IEndpointRouteBuilder app)
    {
        //GET /api/products
        app.MapGet("api/products", async (IProductsService productsService) =>
        {
            List<ProductResponse?> productResponses = await productsService.GetProducts();
            return Results.Ok(productResponses);
        });

        //GET /api/products/search/product-id/000000-0000-0000-000000000
        app.MapGet("/api/products/search/product-id/{ProductId:guid}", async (IProductsService productsService, Guid ProductId) =>
        {
            ProductResponse? productResponse = await productsService.GetProductByCondition(product => product.ProductID == ProductId);
            return Results.Ok(productResponse);
        });

        //GET /api/products/seach/xxxxxxxx
        app.MapGet("/api/products/search/{SearchString}", async (IProductsService productsService, string searchString) =>
        {
            List<ProductResponse?> productsByName = await productsService.GetProductsByCondition(product => product.ProductName != null
                                                && product.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            List<ProductResponse?> productsByCategory = await productsService.GetProductsByCondition(product => product.Category != null
                                                && product.Category.Contains(searchString, StringComparison.OrdinalIgnoreCase));

            var products = productsByName.Union(productsByCategory);
            return Results.Ok(products);
        });

        //POST /api/products
        app.MapPost("/api/products", async (IProductsService productsService,
                    IValidator<ProductAddRequest> productAddRequestValidator,
                    ProductAddRequest productAddRequest) =>
        {
            ValidationResult validationResult = await productAddRequestValidator.ValidateAsync(productAddRequest);
            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errorList = validationResult.Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(grp => grp.Key, grp => grp.Select(msg => msg.ErrorMessage).ToArray());

                return Results.ValidationProblem(errorList);
            }
            ProductResponse? addedProduct = await productsService.AddProduct(productAddRequest);
            if (addedProduct != null) return Results.Created($"/api/products/search/product-id/{addedProduct.ProductID}", addedProduct);
            else
                return Results.Problem("Error while adding product");
        });

        //DELETE /api/products
        app.MapDelete("/api/products/{ProductId:guid}", async (IProductsService productsService, Guid ProductId) =>
        {
            bool deletedProduct = await productsService.DeleteProduct(ProductId);
            if (!deletedProduct) Results.Problem("Unable to delete the product");
            return Results.Ok(deletedProduct);
        });

        //PUT /api/products
        app.MapPut("/api/products", async (IProductsService productsService,
                    IValidator<ProductUpdateRequest> productUpdateRequestValidator,
                    ProductUpdateRequest productUpdateRequest) =>
        {
            ValidationResult validationResult = await productUpdateRequestValidator.ValidateAsync(productUpdateRequest);
            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errorList = validationResult.Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(grp => grp.Key, grp => grp.Select(msg => msg.ErrorMessage).ToArray());

                return Results.ValidationProblem(errorList);
            }
            ProductResponse? updatedProduct = await productsService.UpdateProduct(productUpdateRequest);
            if (updatedProduct != null) 
                //return Results.Created($"/api/products/search/product-id/{updatedProduct.ProductID}", updatedProduct);
                return Results.Ok(updatedProduct);
            else
                return Results.Problem("Error while updating the product");
        });
        return app;
    }
}
