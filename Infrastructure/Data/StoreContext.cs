using System.Reflection;
using Core.Entities;
using Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext>Parameters) : base(Parameters)
        {
            
        }
        
        public DbSet<Product> Products {get; set;}
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        // this is the method responsible for creating the migrations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());*/
            new ProductConfiguration().Configure(modelBuilder.Entity<Product>());
        }
    }
}