namespace SaleTrackerBackend.Dto;

using System.ComponentModel.DataAnnotations;

public class CreateSaleDto
{
  [Required]
  public Guid CustomerId { get; set; }
  
  [Required]
  public List<CreateProductSaleForSaleDto> ProductSales { get; set; } = [];
}
