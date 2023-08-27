using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Core.Entities.Products.ValueObjects;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Write;

internal sealed class OrderWriteConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value,
                guid => new OrderId(guid));

        builder.OwnsOne(x => x.Price, navigationBuilder =>
        {
            navigationBuilder.Property(x => x.Amount).IsRequired().HasColumnName("Amount");
            navigationBuilder.Property(x => x.Currency).IsRequired().HasColumnName("Currency");
        });

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion(x => x.Value,
                s => new OrderStatus(s));

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.CustomerId);

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(x => x.ProductId);

        builder.Property(x => x.CustomerId)
            .IsRequired()
            .HasConversion(x => x.Value,
                guid => new UserId(guid));

        builder.Property(x => x.ProductId)
            .IsRequired()
            .HasConversion(x => x.Value,
                guid => new ProductId(guid));
    }
}