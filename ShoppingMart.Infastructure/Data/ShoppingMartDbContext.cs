using Microsoft.EntityFrameworkCore;
using ShoppingMart.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMart.Infastructure.Data
{
    public class ShoppingMartDbContext : DbContext
    {
        public ShoppingMartDbContext(DbContextOptions<ShoppingMartDbContext> options) : base(options)
        {

        }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(f => f.CategoryForeignKey);
            modelbuilder.ApplyConfiguration(new ProductEntityConfig());
            modelbuilder.ApplyConfiguration(new CategoryEntityConfig());
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

    }
}
