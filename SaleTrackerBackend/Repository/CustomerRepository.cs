namespace SaleTrackerBackend.Repository;

using Microsoft.EntityFrameworkCore;
using SaleTrackerBackend.Data;
using SaleTrackerBackend.Models;

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

  public async Task<Customer?> GetByIdAsync(int id)
  {
    try
    {
      return await db.Customers.Where(c => c.CustomerId == id && !c.IsDeleted).FirstOrDefaultAsync();
    }
    catch (Exception)
    {
      throw new Exception("Cannot get customer");
    }
  }

  public async Task<List<Customer>?> GetAllAsync(int page, int count)
  {
    try
    {
      var customers = db.Customers.Where(c => !c.IsDeleted).Skip((page - 1) * count).Take(count).OrderBy(c => c.CustomerId);
      return await customers.ToListAsync();
    }
    catch (Exception)
    {
      return null;
    }
  }


  public async Task CreateAsync(Customer customer)
  {
    try
    {
      await db.Customers.AddAsync(customer);
      await SaveAsync();
    }
    catch (Exception ex)
    {
      throw new Exception("Failed to create customer", ex);
    }
  }

  public async Task UpdateAsync(Customer customer)
  {
    try
    {
      var currentCustomer = await GetByIdAsync(customer.CustomerId);
      if (currentCustomer is null)
      {
        throw new Exception("Customer not found");
      }
      currentCustomer.Name = customer.Name;
      currentCustomer.Phone = customer.Phone;
      currentCustomer.Address = customer.Address;
      currentCustomer.UpdatedOn = DateTime.Now;
      await SaveAsync();
    }
    catch (Exception ex)
    {
      throw new Exception("Failed to update customer =>" + ex.Message, ex);
    }
  }

  public async Task MarkDeletedAsync(int id)
  {
    try
    {
      var customer = await db.Customers.FindAsync(id);
      if (customer is null)
      {
        throw new Exception("Customer not found");
      }
      customer.IsDeleted = true;
      await SaveAsync();
    }
    catch (Exception ex)
    {
      throw new Exception("Failed to delete customer => " + ex.Message, ex);
    }
  }

  public async Task DeletePermanently(int id)
  {
    try
    {
      var customer = await db.Customers.FindAsync(id);
      if (customer is null)
      {
        throw new Exception("Customer not found");
      }
      db.Customers.Remove(customer);
      await SaveAsync();
    }
    catch (Exception ex)
    {
      throw new Exception("Failed to delete customer", ex);
    }
  }
}