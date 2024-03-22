namespace SaleTrackerBackend.Models.Dto;

public class GetProductDto
{

    public int ProductId { get; set; }

    public string Name { get; set; } = "";
    public string Description { get; set; } = "";

    public decimal Price { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime UpdatedOn { get; set; } = DateTime.Now;
}