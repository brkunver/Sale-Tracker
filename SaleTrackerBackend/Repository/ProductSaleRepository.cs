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
      var sale = await db.Sales.FirstOrDefaultAsync(s => s.Id == SaleId);
      if (sale is null)
      {
        throw new Exception("Sale not found");
      }
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



}