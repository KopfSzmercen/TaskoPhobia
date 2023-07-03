﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

namespace TaskoPhobia.Infrastructure.DAL.Configurations.Read;

internal class ProjectTaskReadConfiguration : IEntityTypeConfiguration<ProjectTaskReadModel>
{
    public void Configure(EntityTypeBuilder<ProjectTaskReadModel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("ProjectTasks");
    }
}