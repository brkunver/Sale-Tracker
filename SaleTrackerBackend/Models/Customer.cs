namespace SaleTrackerBackend.Models;

public partial class Customer
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";

    public string Phone { get; set; } = "";

    public string Address { get; set; } = "";

    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public DateTime? UpdatedOn { get; set; } = DateTime.Now;

    public bool IsDeleted { get; set; } = false;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
