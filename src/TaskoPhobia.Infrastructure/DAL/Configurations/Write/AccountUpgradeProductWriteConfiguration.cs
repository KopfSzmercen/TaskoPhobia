using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts.ValueObjects;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Write;

internal sealed class AccountUpgradeProductWriteConfiguration : IEntityTypeConfiguration<AccountUpgradeProduct>
{
    public void Configure(EntityTypeBuilder<AccountUpgradeProduct> builder)
    {
        builder.Property(x => x.UpgradeTypeValue)
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new AccountUpgradeTypeValue(x));

        builder.HasIndex(x => x.UpgradeTypeValue)
            .IsUnique();
    }
}