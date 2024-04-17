namespace SaleTrackerBackend.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Address { get; set; } = "";
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime UpdatedOn { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
}