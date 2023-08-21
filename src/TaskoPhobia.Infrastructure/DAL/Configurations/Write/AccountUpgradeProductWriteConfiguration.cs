using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts.ValueObjects;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Write;

public class AccountUpgradeProductWriteConfiguration : IEntityTypeConfiguration<AccountUpgradeProduct>
{
    public void Configure(EntityTypeBuilder<AccountUpgradeProduct> builder)
    {
        /*
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new ProductId(x));
                */

        builder.Property(x => x.UpgradeTypeValue)
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new AccountUpgradeTypeValue(x));

        builder.HasIndex(x => x.UpgradeTypeValue)
            .IsUnique();

        /*
        builder.Property(x => x.Description)
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new ProductDescription(x));

        builder.Property(x => x.Name)
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new ProductName(x));

            */
        /* builder.OwnsOne(x => x.Price, money =>
         {
             money.Property(x => x.Amount).IsRequired().HasColumnName("Amount");
             money.Property(x => x.Currency).IsRequired().HasColumnName("Currency");
         }); */
    }
}