namespace SaleTrackerBackend.Models.Dto;
using System.ComponentModel.DataAnnotations;

public class CreateCustomerDto
{
  [Required]
  [MaxLength(100)]
  public required string Name { get; set; }
  [Required]
  [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid phone number"), MaxLength(20)]
  public required string Phone { get; set; }
  [Required]
  [MaxLength(500)]
  public required string Address { get; set; }
}