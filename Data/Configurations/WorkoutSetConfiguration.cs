using FitTrack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTrack.Data.Configurations;

public class WorkoutSetConfiguration : IEntityTypeConfiguration<WorkoutSet>
{
    public void Configure(EntityTypeBuilder<WorkoutSet> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.SetNumber).IsRequired();
        builder.Property(s => s.Reps).IsRequired();
        builder.Property(s => s.WeightKg).IsRequired();

        builder.HasOne(s => s.Exercise)
            .WithMany(e => e.WorkoutSets)
            .HasForeignKey(s => s.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(s => new { s.WorkoutId, s.ExerciseId, s.SetNumber }).IsUnique();
    }
}
