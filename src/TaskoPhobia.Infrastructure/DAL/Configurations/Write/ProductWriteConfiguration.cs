using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Core.Entities.Products.ValueObjects;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Write;

public class ProductWriteConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new ProductId(x));

        builder.Property(x => x.Description)
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new ProductDescription(x));

        builder.Property(x => x.Name)
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new ProductName(x));

        builder.OwnsOne(x => x.Price, money =>
        {
            money.Property(x => x.Amount).HasColumnName("Amount");
            money.Property(x => x.Currency).HasColumnName("Currency");
        });
    }
}