namespace SaleTrackerBackend.Models.Dto;

public class GetSaleDto
{
    public int SaleId { get; set; }
    public DateTime SaledOn { get; set; } = DateTime.Now;

    public int CustomerId { get; set; }

    public List<SaleDetail> SaleDetails { get; set; } = [];

    public decimal Total { get; set; }
}
