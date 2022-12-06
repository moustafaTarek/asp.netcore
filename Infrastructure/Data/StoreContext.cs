using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext>Parameters) : base(Parameters)
        {
            
        }
        
        public DbSet<Product> Products {get; set;}
    }
}