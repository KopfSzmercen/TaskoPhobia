using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Read;

internal sealed class UserReadConfiguration : IEntityTypeConfiguration<UserReadModel>
{
    public void Configure(EntityTypeBuilder<UserReadModel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Users");
    }
}