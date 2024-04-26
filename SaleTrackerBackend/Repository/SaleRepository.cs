// namespace SaleTrackerBackend.Repository;

// using Microsoft.EntityFrameworkCore;
// using SaleTrackerBackend.Data;
// using SaleTrackerBackend.Models;
// using SaleTrackerBackend.Models.Dto;

// public class SaleRepository
// {
//   private readonly DataContext db;
//   public SaleRepository(DataContext dataContext)
//   {
//     db = dataContext;
//   }

//   public async Task SaveAsync()
//   {
//     try
//     {
//       await db.SaveChangesAsync();
//     }
//     catch (Exception)
//     {
//       throw new Exception("Cannot save changes");
//     }
//   }

//   public async Task<Sale?> GetSaleByIdAsync(int id)
//   { 
//     try
//     {
//       return await db.Sales.Include(sale => sale.Customer).Include(sale => sale.SaleDetails).FirstOrDefaultAsync(s => s.SaleId == id);
//     }


//     catch (Exception)
//     {
//       throw new Exception("Failed to get sales");
//     }
//   }



// }