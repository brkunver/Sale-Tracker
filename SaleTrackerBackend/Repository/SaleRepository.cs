namespace SaleTrackerBackend.Repository;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SaleTrackerBackend.Data;
using SaleTrackerBackend.Models;
using SaleTrackerBackend.Models.Dto;

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

    public async Task<List<Sale>?> GetAllAsync(int page, int count)
    {
        try
        {
            var sales = db.Sales.Skip((page - 1) * count).Take(count).OrderBy(s => s.SaleId);
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

    public async Task<List<GetSaleWithProductDto>?> GetAllWithProductAsync(int page, int count)
    {
        try
        {
            var sales = await db.Sales
                .Include(s => s.Product)
                .Skip((page - 1) * count)
                .Take(count)
                .OrderBy(s => s.SaleId)
                .ToListAsync();

            if (sales.Count == 0)
            {
                return [];
            }
    
            var dto = sales.Select(s => new GetSaleWithProductDto
            {
                SaleId = s.SaleId,
                SaledOn = s.SaledOn,
                ProductId = s.ProductId,
                ProductName = s.Product.Name,
                ProductDescription = s.Product.Description,
                ProductPrice = s.Product.Price,
                ProductCreatedOn = s.Product.CreatedOn,
                ProductUpdatedOn = s.Product.UpdatedOn,
                ProductImageUrl = s.Product.ImageUrl
            }).ToList();

            return dto;
        }
        catch (Exception)
        {
            return null;
        }
    }
}