namespace SaleTrackerBackend.Models;

public partial class ProductSale
{
    public Guid ProductId { get; set; }

    public Guid SaleId { get; set; }

    public int Quantity { get; set; }

    public decimal SaledPrice { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Product Product { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Sale Sale { get; set; } = null!;
}
