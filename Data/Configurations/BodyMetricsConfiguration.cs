using FitTrack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTrack.Data.Configurations;

public class BodyMetricsConfiguration : IEntityTypeConfiguration<BodyMetrics>
{
    public void Configure(EntityTypeBuilder<BodyMetrics> builder)
    {
        builder.HasKey(bm => bm.Id);

        builder.Property(bm => bm.HeightCm).IsRequired();
        builder.Property(bm => bm.WeightKg).IsRequired();
        builder.Property(bm => bm.Goal).HasConversion<string>().HasMaxLength(30);
        builder.Property(bm => bm.UpdatedAt).IsRequired();

        builder.HasOne(bm => bm.User)
            .WithOne(u => u.BodyMetrics)
            .HasForeignKey<BodyMetrics>(bm => bm.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(bm => bm.UserId).IsUnique();
    }
}
