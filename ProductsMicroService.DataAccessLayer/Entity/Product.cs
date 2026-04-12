using System.ComponentModel.DataAnnotations;

namespace ProductsMicroService.DataAccessLayer.Entity;
public class Product
{
    [Key]
    public Guid ProductID { get; set; }
    [Required]
    public string ProductName { get; set; }
    [Required]
    public string Category { get; set; }
    public double? UnitPrice { get; set; }
    public int? QuantityInStock { get; set; }

}
