namespace SaleTrackerBackend.Models.Dto;

using System.ComponentModel.DataAnnotations;

public class CreateSaleDto
{
    [Required]
    public int? ProductId { get; set; }
    public DateTime? SaledOn { get; set; } = DateTime.Now;
}