using System.ComponentModel.DataAnnotations;

namespace SaleTrackerBackend.Dto;

public class CreateProductDto
{
  [Required]
  public string Name { get; set; } = "";

  public string Description { get; set; } = "";

  public decimal Price { get; set; } = 0;

  public IFormFile? FormFile { get; set; }
}