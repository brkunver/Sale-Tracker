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

  public async Task<List<Sale>?> GetSalesAsync(int count, int page)
  {
    try
    {
      int skip = (page - 1) * count;
      var sales = await db.Sales.AsNoTracking()
                           .OrderByDescending(s => s.SaledOn)
                           .Skip(skip)
                           .Take(count)
                           .ToListAsync();
      return sales;
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
      var customer = await db.Customers.FirstOrDefaultAsync(c => c.Id == sale.CustomerId) ?? throw new Exception("Customer not found");
      var newSale = await db.Sales.AddAsync(sale);
      await SaveAsync();
      return newSale.Entity;
    }
    catch (Exception)
    {
      throw new Exception("Failed to create sale");
    }
  }

  public async Task<Sale?> UpdateSaleAsync(Guid id, Sale sale)
  {
    try
    {
      var saleToUpdate = await db.Sales.FirstOrDefaultAsync(s => s.Id == id);
      if (saleToUpdate is null)
      {
        throw new Exception("Sale not found");
      }
      saleToUpdate.CustomerId = sale.CustomerId;
      saleToUpdate.SaledOn = sale.SaledOn;
      saleToUpdate.Total = sale.Total;
      await SaveAsync();
      return saleToUpdate;
    }
    catch (Exception)
    {
      throw new Exception("Failed to update sale");
    }
  }

  public async Task DeleteSaleAsync(Guid id)
  {
    try
    {
      var sale = await db.Sales.FirstOrDefaultAsync(s => s.Id == id);
      if (sale is null)
      {
        throw new Exception("Sale not found");
      }
      db.Sales.Remove(sale);
      await SaveAsync();
    }
    catch (Exception)
    {
      throw new Exception("Failed to delete sale");
    }
  }

  public async Task<List<decimal>?> GetLastSalesAsync(int count = 7)
  {
    try
    {
      return await db.Sales.OrderByDescending(s => s.SaledOn)
                           .Take(count)
                           .Select(s => s.Total)
                           .ToListAsync();
    }
    catch (Exception)
    {
      throw new Exception("Failed to get last sales");
    }
  }

  public async Task<decimal> GetSumOfLastSalesAsync(int days = 7)
  {
    try
    {
      DateTime startDate = DateTime.Now.AddDays(-days);
      decimal sum = await db.Sales.Where(s => s.SaledOn >= startDate)
                     .SumAsync(s => s.Total);
      return sum;
    }
    catch (Exception)
    {
      throw new Exception("Failed to get sum of last sales");
    }
  }
}