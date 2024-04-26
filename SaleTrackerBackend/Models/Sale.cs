namespace SaleTrackerBackend.Models;

public partial class Sale
{
    public Guid Id { get; set; }

    public DateTime? SaledOn { get; set; }

    public Guid? CustomerId { get; set; }

    public decimal Total { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<ProductSale> ProductSales { get; set; } = new List<ProductSale>();
}
