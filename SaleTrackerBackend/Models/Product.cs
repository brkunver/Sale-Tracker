namespace SaleTrackerBackend.Models;

public partial class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public decimal Price { get; set; } 

    public DateTime CreatedOn { get; set; } = DateTime.Now;
 
    public DateTime UpdatedOn { get; set; } = DateTime.Now;

    public string ImageUrl { get; set; } = "default.jpg";

    public bool IsDeleted { get; set; } = false;

    public virtual ICollection<ProductSale> ProductSales { get; set; } = new List<ProductSale>();
}
