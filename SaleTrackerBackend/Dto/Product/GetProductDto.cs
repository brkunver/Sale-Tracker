namespace SaleTrackerBackend.Dto;

public class GetProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; } 
    public DateTime CreatedOn { get; set; } 
    public DateTime UpdatedOn { get; set; } 
    public string ImageUrl { get; set; } = "default.jpg";
}
