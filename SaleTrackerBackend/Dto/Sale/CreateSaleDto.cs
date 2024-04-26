namespace SaleTrackerBackend.Models.Dto;

using System.ComponentModel.DataAnnotations;

public class CreateSaleDto
{
    [Required]
    public int? ProductId { get; set; }
    public DateTime? SaledOn { get; set; } = DateTime.Now;
    [Required]
    public int? Quantity { get; set; }
    [Required]
    public decimal? Total { get; set; }
    [Required]
    public int CustomerId { get; set; }
}