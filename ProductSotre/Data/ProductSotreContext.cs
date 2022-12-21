using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using ProductSotre.Data;

namespace DAL.Data
{
    public class ProductSotreContext : PSContext
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasKey(x => x.ProductID);
            
            modelBuilder.Entity<Category>()
                .HasKey(x => x.CategoryID);
        }
    }
}