using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class ProductSotreContext : DbContext
    {
        public ProductSotreContext (DbContextOptions<ProductSotreContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = default!;

        public DbSet<Category> Categories { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(x => x.ProductID);
            
            modelBuilder.Entity<Category>()
                .HasKey(x => x.CategoryID);
        }
    }
}