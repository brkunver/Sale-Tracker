namespace SaleTrackerBackend.Models.Dto;

public class GetSaleDto
{
    public int SaleId { get; set; }
    public DateTime SaledOn { get; set; } = DateTime.Now;
    public int ProductId { get; set; }
}
