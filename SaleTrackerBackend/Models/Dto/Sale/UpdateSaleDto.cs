namespace SaleTrackerBackend.Models.Dto;

public class UpdateSaleDto
{
    public DateTime? SaledOn { get; set; } = DateTime.Now;
    public int ProductId { get; set; }
}