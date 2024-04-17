using System.ComponentModel.DataAnnotations.Schema;

namespace SaleTrackerBackend.Models;

public class Sale
{
    public int SaleId { get; set; }
    public DateTime SaledOn { get; set; } = DateTime.Now;
   
    public int? ProductId { get; set; }
    public Product Product { get; set; } = new();

    public int? CustomerId { get; set; }
    public Customer Customer { get; set; } = new();
    
    public int Quantity { get; set; }
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Total { get; set; }
}