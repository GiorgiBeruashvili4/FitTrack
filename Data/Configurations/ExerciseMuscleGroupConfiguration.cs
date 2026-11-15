using FitTrack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTrack.Data.Configurations;

public class ExerciseMuscleGroupConfiguration : IEntityTypeConfiguration<ExerciseMuscleGroup>
{
    public void Configure(EntityTypeBuilder<ExerciseMuscleGroup> builder)
    {
        builder.HasKey(x => new { x.ExerciseId, x.MuscleGroupId });

        builder.HasOne(x => x.Exercise)
            .WithMany(e => e.ExerciseMuscleGroups)
            .HasForeignKey(x => x.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.MuscleGroup)
            .WithMany(m => m.ExerciseMuscleGroups)
            .HasForeignKey(x => x.MuscleGroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
