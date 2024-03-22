namespace SaleTrackerBackend.Models.Dto;

public class CreateSaleDto
{
    public int? ProductId { get; set; }
    public DateTime? SaledOn { get; set; } = DateTime.Now;
}