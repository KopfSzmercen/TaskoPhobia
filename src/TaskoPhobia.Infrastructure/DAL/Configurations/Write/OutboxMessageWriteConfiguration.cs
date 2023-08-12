using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Shared.Abstractions.Outbox;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Write;

internal sealed class OutboxMessageWriteConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(x => x.Id);
    }
}