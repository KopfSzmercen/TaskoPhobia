using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Core.Entities.Payments;
using TaskoPhobia.Core.Entities.Payments.ValueObjects;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Url;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Write;

internal sealed class PaymentWriteConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new PaymentId(x));

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new PaymentStatus(x));

        builder.Property(x => x.RedirectUrl)
            .HasConversion(x => x.Value,
                x => new Url(x));

        builder.HasOne<Order>()
            .WithMany()
            .HasForeignKey(x => x.OrderId);

        builder.Property(x => x.PaidAt);

        builder.OwnsOne(x => x.MoneyToPay, navigationBuilder =>
        {
            navigationBuilder.Property(x => x.Amount).IsRequired().HasColumnName("Amount");
            navigationBuilder.Property(x => x.Currency).IsRequired().HasColumnName("Currency");
        });

        builder.Property<uint>("Version").IsRowVersion();
    }
}