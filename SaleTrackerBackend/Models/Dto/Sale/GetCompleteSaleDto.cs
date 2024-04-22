namespace SaleTrackerBackend.Models.Dto;


public class GetCompleteSaleDto
{
  public int SaleId { get; set; }
  public DateTime SaledOn { get; set; }
  
  public int? ProductId { get; set; }
  public string ProductName { get; set; } = "";
  public string ProductDescription { get; set; } = "";
  public decimal ProductPrice { get; set; }
  public DateTime ProductCreatedOn { get; set; }
  public DateTime ProductUpdatedOn { get; set; }
  public string ProductImageUrl { get; set; } = "default.jpg";

  public int CustomerId { get; set; }
  public string CustomerName { get; set; } = "";
  public string CustomerPhone { get; set; } = "";
  public string CustomerAddress { get; set; } = "";

}