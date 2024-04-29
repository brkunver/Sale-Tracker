namespace SaleTrackerBackend.Dto;

public partial class GetSaleDto
{
    public Guid Id { get; set; }

    public DateTime SaledOn { get; set; } 

    public Guid CustomerId { get; set; }

    public decimal Total { get; set; }

}
