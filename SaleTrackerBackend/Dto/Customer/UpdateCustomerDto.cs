using System.ComponentModel.DataAnnotations;

namespace SaleTrackerBackend.Dto;

public partial class UpdateCustomerDto
{
  [Required]
  public string Name { get; set; } = "";
  [Required]
  public string Phone { get; set; } = "";
  [Required]
  public string Address { get; set; } = "";

  public IFormFile? FormFile { get; set; }
}
