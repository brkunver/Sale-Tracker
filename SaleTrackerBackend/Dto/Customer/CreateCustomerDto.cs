using System.ComponentModel.DataAnnotations;

namespace SaleTrackerBackend.Dto;

public partial class CreateCustomerDto
{
  [Required]
  [MaxLength(255)]
  public string Name { get; set; } = "";

  [Required]
  [RegularExpression(@"^[0-9]{1,20}$")]
  public string Phone { get; set; } = "";

  [Required]
  [MaxLength(500)]
  public string Address { get; set; } = "";


}
