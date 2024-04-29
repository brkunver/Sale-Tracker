using System.ComponentModel.DataAnnotations;

namespace SaleTrackerBackend.Dto;

public partial class CreateSaleSale
{
  [Required]
  public Guid CustomerId { get; set; }
  [Required]
  public decimal Total { get; set; }
}
