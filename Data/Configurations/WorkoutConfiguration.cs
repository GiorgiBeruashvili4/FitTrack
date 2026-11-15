using FitTrack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTrack.Data.Configurations;

public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
{
    public void Configure(EntityTypeBuilder<Workout> builder)
    {
        builder.HasKey(w => w.Id);

        builder.Property(w => w.Date).IsRequired();
        builder.Property(w => w.Title).IsRequired().HasMaxLength(80);
        builder.Property(w => w.Notes).HasMaxLength(1000);
        builder.Property(w => w.DurationMinutes).IsRequired();

        builder.HasMany(w => w.WorkoutSets)
            .WithOne(s => s.Workout)
            .HasForeignKey(s => s.WorkoutId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(w => new { w.UserId, w.Date });
    }
}
