using Microsoft.EntityFrameworkCore;
using SaleTrackerBackend.Data;
using SaleTrackerBackend.Models;

namespace SaleTrackerBackend.Repository;

public class ProductRepository
{
    private readonly DataContext db;
    public ProductRepository(DataContext context)
    {
        db = context;
    }

    public async Task<bool> CreateAsync(Product product)
    {
        try
        {
            var newProduct = await db.Products.AddAsync(product);
            await SaveAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool?> SaveAsync()
    {
        try
        {
            await db.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        try
        {
            var product = await db.Products.FindAsync(id);
            return product;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<List<Product>?> GetAllAsync(int page)
    {
        try
        {
            var products = db.Products.Skip((page - 1) * 10).Take(10).OrderBy(p => p.ProductId);
            return await products.ToListAsync();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<int> GetCountAsync()
    {
        try
        {
            return await db.Products.CountAsync();
        }
        catch (Exception)
        {
            throw new Exception("Get count error");
        }
    }

    public async Task<bool> UpdateAsync(int id, Product product)
    {
        try
        {
            var prod = await db.Products.FindAsync(id);
            if (prod != null)
            {
                prod.Name = product.Name;
                prod.Description = product.Description;
                prod.Price = product.Price;
                prod.UpdatedOn = DateTime.Now;
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

    public async Task<bool> DeleteByIdAsync(int id)
    {
        try
        {
            var prod = await db.Products.FindAsync(id);
            if (prod is null)
            {
                return false;
            }
            db.Products.Remove(prod);
            await SaveAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}