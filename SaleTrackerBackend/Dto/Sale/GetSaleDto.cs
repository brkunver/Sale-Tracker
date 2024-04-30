namespace SaleTrackerBackend.Dto;

public class GetSaleDto
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public DateTime SaledOn { get; set; }

    public decimal Total { get; set; }

}
