using System.ComponentModel.DataAnnotations;

namespace SaleTrackerBackend.Dto;

public partial class CreateSaleDto
{
  [Required]
  public Guid CustomerId { get; set; }
  [Required]
  public decimal Total { get; set; }
}
