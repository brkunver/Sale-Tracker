namespace SaleTrackerBackend.Dto;

public class UpdateSaleDto
{
  public DateTime SaledOn { get; set; }
  public Guid CustomerId { get; set; }
  public decimal Total { get; set; }
}
