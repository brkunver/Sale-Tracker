using Microsoft.EntityFrameworkCore;
using SaleTrackerBackend.Models;

namespace SaleTrackerBackend.Repository;

public class ProductRepository
{
    private readonly SaletrackerContext db;
    public ProductRepository(SaletrackerContext context)
    {
        db = context;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        try
        {
            var newProduct = await db.Products.AddAsync(product);
            await SaveAsync();
            return newProduct.Entity;
        }
        catch (Exception)
        {
            throw new Exception("Failed to create product");
        }
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

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        try
        {
            var product = await db.Products.Where(p => p.Id == id && !p.IsDeleted).FirstOrDefaultAsync();
            return product;
        }
        catch (Exception)
        {
            throw new Exception("Failed to get product");
        }
    }

    public async Task<List<Product>?> GetAllAsync(int page, int count)
    {
        try
        {
            var products = db.Products.Where(p => !p.IsDeleted).Skip((page - 1) * count).Take(count).OrderBy(p => p.CreatedOn);
            return await products.ToListAsync();
        }
        catch (Exception)
        {
            throw new Exception("Failed to get products");
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
            throw new Exception("Failed to get count of products");
        }
    }

    public async Task UpdateAsync(Guid id, Product product)
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
        catch (Exception e)
        {
            throw new Exception("Failed to update product," + e.Message);
        }
    }

    public async Task DeleteByIdAsync(Guid id)
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
        catch (Exception e)
        {
            throw new Exception("Failed to delete product," + e.Message);
        }
    }

    public async Task MarkDeletedAsync(Guid id)
    {
        try
        {
            var prod = await db.Products.FindAsync(id) ?? throw new Exception("Product not found");
            prod.IsDeleted = true;
            await SaveAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Failed to delete product," + e.Message);
        }
    }
}