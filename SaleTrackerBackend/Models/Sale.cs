namespace SaleTrackerBackend.Models;

public partial class Sale
{
    public Guid Id { get; set; }

    public DateTime SaledOn { get; set; } = DateTime.Now;

    public Guid CustomerId { get; set; }

    public decimal Total { get; set; }

    public virtual required Customer Customer { get; set; }

    public virtual ICollection<ProductSale> ProductSales { get; set; } = new List<ProductSale>();
}
