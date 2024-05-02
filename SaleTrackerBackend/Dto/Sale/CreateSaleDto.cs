namespace SaleTrackerBackend.Dto;

using System.ComponentModel.DataAnnotations;

public class CreateSaleDto
{
  [Required]
  public Guid CustomerId { get; set; }

  public DateTime? SaledOn { get; set; } = DateTime.Now;

  [Required]
  public List<CreateProductSaleForSaleDto> ProductSales { get; set; } = [];
}
