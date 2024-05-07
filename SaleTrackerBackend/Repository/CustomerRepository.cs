namespace SaleTrackerBackend.Repository;

using Microsoft.EntityFrameworkCore;
using SaleTrackerBackend.Dto;
using SaleTrackerBackend.Models;

public class CustomerRepository
{
  private readonly SaletrackerContext db;
  public CustomerRepository(SaletrackerContext dataContext)
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

  public async Task<Customer?> GetByIdAsync(Guid id, bool includeDeleted = false)
  {
    try
    {
      var customer = includeDeleted ?
      await db.Customers.Where(c => c.Id == id).FirstOrDefaultAsync() :
      await db.Customers.Where(c => c.Id == id && !c.IsDeleted).FirstOrDefaultAsync();
      return customer;
    }
    catch (Exception)
    {
      throw new Exception("Failed to get customer");
    }
  }



  public async Task<List<Customer>?> GetAllAsync(int page, int count, string? name = null, bool returnDeleted = false)
  {
    try
    {
      var customers = db.Customers.AsQueryable();

      if (returnDeleted is false)
      {
        customers = customers.Where(c => !c.IsDeleted);
      }

      if (!string.IsNullOrEmpty(name))
      {
        customers = customers.Where(c => c.Name.Contains(name));
      }

      customers = customers.Skip((page - 1) * count).Take(count).OrderBy(c => c.CreatedOn);

      return await customers.ToListAsync();
    }
    catch (Exception)
    {
      throw new Exception("Failed to get customers");
    }
  }


  public async Task CreateAsync(Customer customer)
  {
    try
    {
      await db.Customers.AddAsync(customer);
      await SaveAsync();
    }
    catch (Exception)
    {
      throw new Exception("Failed to create customer");
    }
  }


  public async Task MarkDeletedAsync(Guid id)
  {
    try
    {
      var customer = await db.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
      if (customer is null)
      {
        throw new Exception("Customer not found");
      }
      customer.IsDeleted = true;
      await SaveAsync();
    }
    catch (Exception e)
    {
      throw new Exception("Failed to delete customer, " + e.Message);
    }
  }

  public async Task DeletePermanently(Guid id)
  {
    try
    {
      var customer = await GetByIdAsync(id);
      if (customer is null)
      {
        throw new Exception("Customer not found");
      }
      db.Customers.Remove(customer);
      await SaveAsync();
    }
    catch (Exception e)
    {
      throw new Exception("Failed to delete customer permanently," + e.Message);
    }
  }


  public async Task<int> GetCountAsync()
  {
    try
    {
      return await db.Customers.Where(c => !c.IsDeleted).CountAsync();
    }
    catch (Exception)
    {
      throw new Exception("Failed to get count of customers");
    }
  }

  public async Task<Customer> UpdateAsync(Guid id, UpdateCustomerDto customerDto)
  {
    try
    {
      var currentCustomer = await GetByIdAsync(id) ?? throw new Exception("Customer not found");
      currentCustomer.Name = customerDto.Name;
      currentCustomer.Phone = customerDto.Phone;
      currentCustomer.Address = customerDto.Address;
      currentCustomer.UpdatedOn = DateTime.Now;
      await SaveAsync();
      return currentCustomer;
    }
    catch (Exception e)
    {
      throw new Exception("Failed to update customer " + e.Message);
    }
  }

}