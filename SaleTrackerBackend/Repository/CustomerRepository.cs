namespace SaleTrackerBackend.Repository;

using Microsoft.EntityFrameworkCore;
using SaleTrackerBackend.Data;
using SaleTrackerBackend.Models;
using SaleTrackerBackend.Models.Dto;

public class CustomerRepository
{
  private readonly DataContext db;
  public CustomerRepository(DataContext dataContext)
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
      throw new Exception("Cannot save changes");
    }
  }

  public async Task<Customer?> GetCustomerByIdAsync(int id)
  {
    return await db.Customers.FirstOrDefaultAsync(customer => customer.CustomerId == id);
  }
}