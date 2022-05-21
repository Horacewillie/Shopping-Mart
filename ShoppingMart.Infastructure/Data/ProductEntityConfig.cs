using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingMart.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMart.Infastructure.Data
{
    public class ProductEntityConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Price)
                .IsRequired()
                .HasColumnType("decimal(18,4)");
            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(g => g.Description)
                .IsRequired();
        }

    }
}
