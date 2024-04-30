namespace SaleTrackerBackend.Dto;

public class GetProductSaleDto
{
  public Guid ProductId { get; set; }

  public Guid SaleId { get; set; }

  public int Quantity { get; set; }

  public decimal SaledPrice { get; set; }

}
