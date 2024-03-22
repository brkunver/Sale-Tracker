using System.ComponentModel.DataAnnotations;

namespace SaleTrackerBackend.Models.Dto;

public class UpdateProductDto
{
    [Required]
    public string Name { get; set; } = "";
    [Required]
    public string Description { get; set; } = "";
    [Required]
    public decimal Price { get; set; }
}