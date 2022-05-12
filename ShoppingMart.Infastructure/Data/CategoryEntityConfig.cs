using ShoppingMart.Domain;
using Microsoft.EntityFrameworkCore;
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
