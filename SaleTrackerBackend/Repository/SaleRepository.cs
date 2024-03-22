namespace SaleTrackerBackend.Repository;

using Microsoft.EntityFrameworkCore;
using SaleTrackerBackend.Data;
using SaleTrackerBackend.Models;

public class SaleRepository
{
    private readonly DataContext db;
    public SaleRepository(DataContext dataContext)
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

    public async Task<bool> CreateAsync(Sale sale)
    {
        try
        {
            if (sale.ProductId == null)
            {
                return false; // Satış yapılacak ürün belirtilmemiş
            }

            // Veritabanından ilgili ürünü alarak Sale nesnesine atayın
            var product = await db.Products.FindAsync(sale.ProductId);
            if (product == null)
            {
                return false; // Ürün bulunamadı
            }

            sale.Product = product; // Sale nesnesine ürünü atayın

            await db.Sales.AddAsync(sale);
            await SaveAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        try
        {
            var sale = await db.Sales.FindAsync(id);
            if (sale is null)
            {
                return false;
            }
            db.Sales.Remove(sale);
            await SaveAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
            throw;
        }
    }

    public async Task<int> GetCountAsync()
    {
        try
        {
            return await db.Sales.CountAsync();
        }
        catch (Exception)
        {
            throw new Exception("Get count error");
        }
    }

    public async Task<bool> UpdateAsync(int id, Sale sale)
    {
        try
        {
            var sale1 = await db.Sales.FindAsync(id);
            if (sale1 != null)
            {
                sale1.SaledOn = DateTime.Now;
                sale1.ProductId = sale.ProductId;
                await SaveAsync();
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<List<Sale>?> GetAllAsync(int page)
    {
        try
        {
            var sales = db.Sales.Skip((page - 1) * 10).Take(10).OrderBy(s => s.SaleId);
            return await sales.ToListAsync();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Sale?> GetByIdAsync(int id)
    {
        try
        {
            var sale = await db.Sales.FindAsync(id);
            return sale;
        }
        catch (Exception)
        {
            return null;
        }
    }
}