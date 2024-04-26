namespace SaleTrackerBackend.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SaleTrackerBackend.Models;

public class DataContext : IdentityDbContext
{

    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<SaleDetail> SaleDetails { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }



}