using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Read;

internal sealed class AccountUpgradeProductReadConfiguration : IEntityTypeConfiguration<AccountUpgradeProductReadModel>
{
    public void Configure(EntityTypeBuilder<AccountUpgradeProductReadModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable("Products");
    }
}