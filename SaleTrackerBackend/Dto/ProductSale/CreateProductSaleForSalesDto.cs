namespace SaleTrackerBackend.Dto;

public class CreateProductSaleForSaleDto
{
  public Guid? SaleId { get; set; }
  public Guid ProductId { get; set; }
  public int Quantity { get; set; }
  public decimal SaledPrice { get; set; }
}