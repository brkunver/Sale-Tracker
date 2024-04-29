namespace SaleTrackerBackend.Repository;

using Microsoft.EntityFrameworkCore;
using SaleTrackerBackend.Models;

public class SaleRepository
{
  private readonly SaletrackerContext db;
  public SaleRepository(SaletrackerContext dataContext)
  {
    db = dataContext;
  }

  public async Task SaveAsync()
  {
    try
    {
      await db.SaveChangesAsync();
    }
    catch (Exception)
    {
      throw new Exception("Failed to save changes");
    }
  }

  public async Task<Sale?> GetSaleByIdAsync(Guid id)
  {
    try
    {
      return await db.Sales.FirstOrDefaultAsync(s => s.Id == id);
    }

    catch (Exception)
    {
      throw new Exception("Failed to get sales");
    }
  }

  public async Task<List<Sale>?> GetSalesAsync()
  {
    try
    {
      return await db.Sales.OrderBy(s => s.SaledOn).ToListAsync();
    }
    catch (Exception)
    {
      throw new Exception("Failed to get sales");
    }
  }

  public async Task<Sale?> CreateSaleAsync(Sale sale)
  {
    try
    {
      var customer = await db.Customers.FirstOrDefaultAsync(c => c.Id == sale.CustomerId);
      if (customer is null)
      {
        throw new Exception("Customer not found");
      }
      await db.Sales.AddAsync(sale);
      await SaveAsync();
      return sale;
    }
    catch (Exception)
    {
      throw new Exception("Failed to create sale");
    }
  }

}