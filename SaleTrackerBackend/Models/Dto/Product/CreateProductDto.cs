namespace SaleTrackerBackend.Models.Dto;
using System.ComponentModel.DataAnnotations;


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