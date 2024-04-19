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

    public async Task CreateAsync(Product product)
    {
        try
        {
            var newProduct = await db.Products.AddAsync(product);
            await SaveAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error creating product: " + ex.Message);
        }
    }

    public async Task SaveAsync()
    {
        try
        {
            await db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error saving changes: " + ex.Message);
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

    public async Task<List<Product>?> GetAllAsync(int page, int count)
    {
        try
        {
            var products = db.Products.Skip((page - 1) * count).Take(count).OrderBy(p => p.ProductId);
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
            return await db.Products.Where(p => !p.IsDeleted).CountAsync();
        }
        catch (Exception)
        {
            throw new Exception("Cannot get product count");
        }
    }

    public async Task UpdateAsync(int id, Product product)
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
                prod.ImageUrl = product.ImageUrl;
                await SaveAsync();
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating product: " + ex.Message);
        }
    }

    public async Task DeleteByIdAsync(int id)
    {
        try
        {
            var prod = await db.Products.FindAsync(id);
            if (prod is null)
            {
                throw new Exception("Product not found");
            }
            db.Products.Remove(prod);
            await SaveAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error deleting product: " + ex.Message);
        }
    }

    public async Task MarkDeletedAsync(int id)
    {
        try
        {
            var prod = await db.Products.FindAsync(id);
            if (prod is null)
            {
                throw new Exception("Product not found");
            }
            prod.IsDeleted = true;
            await SaveAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error deleting product: " + ex.Message);
        }
    }
}