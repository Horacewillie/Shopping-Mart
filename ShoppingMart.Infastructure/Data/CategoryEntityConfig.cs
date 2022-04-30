using System;
using System.Collections.Generic;
using ShoppingMart.Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoopingMart.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingMart.Infastructure.Data
{
    public class CategoryEntityConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(g => g.Name)
               .IsRequired()
               .HasMaxLength(20);
        }
    }
}
