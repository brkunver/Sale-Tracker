namespace SaleTrackerBackend.Models.Dto;
using System.ComponentModel.DataAnnotations;


public class UpdateSaleDto
{
    public DateTime? SaledOn { get; set; } = DateTime.Now;
    [Required]
    public required int ProductId { get; set; }
}