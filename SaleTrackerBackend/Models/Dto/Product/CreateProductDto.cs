using System.ComponentModel.DataAnnotations;

namespace SaleTrackerBackend.Models.Dto;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; } = "";
    [Required]
    public string Description { get; set; } = "";
    [Required]
    public decimal Price { get; set; }

    public IFormFile? FormFile { get; set; }
}