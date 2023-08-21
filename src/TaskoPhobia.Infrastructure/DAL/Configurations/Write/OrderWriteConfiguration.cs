using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Core.Entities.Products.ValueObjects;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Write;

public class OrderWriteConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value,
                x => new OrderId(x));

        builder.Property(x => x.ProductId)
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new ProductId(x));

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(x => x.ProductId);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.OwnsOne(x => x.Price, money =>
        {
            money.Property(x => x.Amount).IsRequired().HasColumnName("Amount");
            money.Property(x => x.Currency).IsRequired().HasColumnName("Currency");
        });

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.CustomerId);

        builder.Property(x => x.CustomerId)
            .IsRequired()
            .HasConversion(x => x.Value, x => new UserId(x));

        builder.Property(x => x.Status)
            .HasConversion(x => x.Value, x => new OrderStatus(x));
    }
}