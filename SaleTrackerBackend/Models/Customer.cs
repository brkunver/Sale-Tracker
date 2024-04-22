namespace SaleTrackerBackend.Models;
using System.ComponentModel.DataAnnotations;


public class Customer
{
    public int CustomerId { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = "";
    [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid phone number"), MaxLength(20)]
    public string Phone { get; set; } = "";
    [MaxLength(500)]
    public string Address { get; set; } = "";
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime UpdatedOn { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
}