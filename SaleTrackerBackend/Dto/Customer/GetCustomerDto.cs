namespace SaleTrackerBackend.Models.Dto;

public class GetCustomerDto
{
  public int CustomerId { get; set; }

  public required string Name { get; set; }
  public required string Phone { get; set; }
  public required string Address { get; set; }
  public DateTime CreatedOn { get; set; }
  public DateTime UpdatedOn { get; set; }
  public bool IsDeleted { get; set; }
}