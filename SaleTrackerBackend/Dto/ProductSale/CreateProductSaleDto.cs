namespace SaleTrackerBackend.Dto;

using System.ComponentModel.DataAnnotations;

public class CreateProductSaleDto
{
  [Required]
  public Guid ProductId { get; set; }
  [Required]
  public Guid SaleId { get; set; }
  [Required]
  public int Quantity { get; set; }
  [Required]
  public decimal SaledPrice { get; set; }
}