namespace SaleTrackerBackend.Models;

public class Sale
{
    public int SaleId { get; set; }
    public DateTime SaledOn { get; set; } = DateTime.Now;
   
    public int? ProductId { get; set; }
    public Product Product { get; set; } = new();
}