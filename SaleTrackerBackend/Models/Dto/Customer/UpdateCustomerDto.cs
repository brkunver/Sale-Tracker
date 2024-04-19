using System.ComponentModel.DataAnnotations;

namespace SaleTrackerBackend.Models.Dto;


public class UpdateCustomerDto
{

    [MaxLength(100)]
    public string? Name { get; set; }
    [MaxLength(500)]
    public string? Address { get; set; }
    [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid phone number"), MaxLength(20)]
    public string? Phone { get; set; }


}