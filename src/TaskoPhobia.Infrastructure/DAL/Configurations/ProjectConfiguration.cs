using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Infrastructure.DAL.Configurations;
// #CR Dotyczy całego DAL, ogólnie ddd i podejście value objectowe jest super ale... wymaga dwóch dbContextów, jednego głównego do zapisu, i drugiego tylko do odczytu, inaczej będziesz się strasznie męczyć z Query

internal sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
// #CR pusta linijka
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, 
                x => new ProjectId(x));

        builder.Property(x => x.Name)
            .IsRequired()
            .HasConversion(x => x.Value, 
                x => new ProjectName(x));


        builder.Property(x => x.Description)
            .HasConversion(x => x.Value,
                x => new ProjectDescription(x));

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion(x => x.Value, 
                x => new ProgressStatus(x));
        
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasOne<User>(project => project.Owner)
            .WithMany(user => user.OwnedProjects)
            .HasForeignKey(project => project.OwnerId);
        
        builder.Property(x => x.OwnerId)
            .HasConversion(x => x.Value, 
                x => new UserId(x));

    }
}