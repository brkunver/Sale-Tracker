namespace SaleTrackerBackend.Repository;

using Microsoft.EntityFrameworkCore;
using SaleTrackerBackend.Models;



public class ProductSaleRepository
{

  private readonly SaletrackerContext db;

  public ProductSaleRepository(SaletrackerContext saletrackerContext)
  {
    db = saletrackerContext;
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

  public async Task<List<ProductSale>?> GetSaleDetailsForSaleAsync(Guid SaleId)
  {
    try
    {
      var sale = await db.Sales.FirstOrDefaultAsync(s => s.Id == SaleId) ?? throw new Exception("Sale not found");
      return await db.ProductSales.Where(ps => ps.SaleId == SaleId).ToListAsync();
    }
    catch (Exception)
    {
      throw new Exception("Failed to get sale details");
    }
  }

  public async Task CreateProductSaleAsync(ProductSale productSale)
  {
    try
    {
      await db.ProductSales.AddAsync(productSale);
      await SaveAsync();
    }
    catch (Exception)
    {
      throw new Exception("Failed to create product sale");
    }
  }

  // Overload method to accept a list of product sales
  public async Task CreateProductSaleAsync(List<ProductSale> productSales)
  {
    try
    {
      await db.ProductSales.AddRangeAsync(productSales);
      await SaveAsync();
    }
    catch (Exception e)
    {
      throw new Exception("Failed to create product sales " + e.Message);
    }
  }


  public async Task<decimal> CalculateTotalForSaleAsync(Guid saleId)
  {
    try
    {
      var productSales = await GetSaleDetailsForSaleAsync(saleId);
      if (productSales is null || productSales.Count == 0)
      {
        throw new Exception("No product sales found for sale");
      }
      return productSales.Sum(ps => ps.SaledPrice * ps.Quantity);
    }
    catch (Exception)
    {
      throw new Exception("Failed to calculate total for sale");
    }
  }


}