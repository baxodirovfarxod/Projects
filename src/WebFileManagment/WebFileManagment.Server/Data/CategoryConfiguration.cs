using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Northwind.Services.EntityFramework.Entities;

namespace Northwind.Services.EntityFramework.Configurations;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(c => c.CategoryID);

        builder.Property(c => c.CategoryID)
               .HasColumnName("CategoryID")
               .ValueGeneratedOnAdd();

        builder.Property(c => c.CategoryName)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(c => c.Description)
               .HasMaxLength(500);

        builder.HasMany(c => c.Products)
               .WithOne(p => p.Category)
               .HasForeignKey(p => p.CategoryID)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
