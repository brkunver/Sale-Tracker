namespace SaleTrackerBackend.Models;
using System.ComponentModel.DataAnnotations.Schema;


public class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = "";
    public string Description { get; set; } = "";

    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime UpdatedOn { get; set; } = DateTime.Now;
    public string ImageUrl { get; set; } = "default.jpg";
}