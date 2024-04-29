namespace SaleTrackerBackend.Dto;
using System.ComponentModel.DataAnnotations;

public class UpdateProductDto
{

  [Required]
  public string Name { get; set; } = "";
  [Required]
  public string Description { get; set; } = "";
  [Required]
  public decimal Price { get; set; }

  public IFormFile? FormFile { get; set; }
}