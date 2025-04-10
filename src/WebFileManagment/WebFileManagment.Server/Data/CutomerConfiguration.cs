using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Services.EntityFramework.Entities;

namespace Northwind.Services.EntityFramework.Configurations;
public class CutomerConfiguration : IEntityTypeConfiguration<Cutomer>
{
    public void Configure(EntityTypeBuilder<Cutomer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.CustomerID);

        builder.Property(c => c.CustomerID)
               .HasColumnName("CustomerID")
               .HasMaxLength(5)
               .IsRequired();

        builder.Property(c => c.CompanyName)
               .HasMaxLength(40)
               .IsRequired();

        builder.Property(c => c.ContactName)
               .HasMaxLength(30);

        builder.Property(c => c.ContactTitle)
               .HasMaxLength(30);

        builder.Property(c => c.Address)
               .HasMaxLength(60);

        builder.Property(c => c.City)
               .HasMaxLength(15);

        builder.Property(c => c.Region)
               .HasMaxLength(15);

        builder.Property(c => c.PostalCode)
               .HasMaxLength(10);

        builder.Property(c => c.Country)
               .HasMaxLength(15);

        builder.Property(c => c.Phone)
               .HasMaxLength(24);

        builder.Property(c => c.Fax)
               .HasMaxLength(24);

        builder.HasMany(c => c.Orders)
               .WithOne(o => o.Customer)
               .HasForeignKey(o => o.CustomerID)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
