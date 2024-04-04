using System.ComponentModel.DataAnnotations;

namespace SaleTrackerBackend.Models.Dto;

public class UpdateSaleDto
{
    public DateTime? SaledOn { get; set; } = DateTime.Now;
    [Required]
    public int ProductId { get; set; }
}