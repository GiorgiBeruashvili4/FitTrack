using FitTrack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTrack.Data.Configurations;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).IsRequired().HasMaxLength(80);
        builder.HasIndex(e => e.Name).IsUnique();

        builder.Property(e => e.Description).HasMaxLength(500);
        builder.Property(e => e.Equipment).HasConversion<string>().HasMaxLength(30);
    }
}
