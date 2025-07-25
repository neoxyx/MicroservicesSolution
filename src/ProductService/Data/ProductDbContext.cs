using Microsoft.EntityFrameworkCore;
using ProductService.Models.Entities;

namespace ProductService.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext() { }
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}