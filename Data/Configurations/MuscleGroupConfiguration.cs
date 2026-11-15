using FitTrack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTrack.Data.Configurations;

public class MuscleGroupConfiguration : IEntityTypeConfiguration<MuscleGroup>
{
    public void Configure(EntityTypeBuilder<MuscleGroup> builder)
    {
        builder.HasKey(mg => mg.Id);

        builder.Property(mg => mg.Name).IsRequired().HasMaxLength(50);

        builder.HasIndex(mg => mg.Name).IsUnique();
    }
}
