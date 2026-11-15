using FitTrack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.SqlServer.Server;

namespace FitTrack.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email).IsRequired().HasMaxLength(200);
        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.FullName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Role).HasConversion<string>().HasMaxLength(20);
        builder.Property(u => u.CreatedAt).IsRequired();

        builder.HasMany(u => u.Workouts)
            .WithOne(w => w.User)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Clients)
            .WithMany(u => u.Trainers)
            .UsingEntity<TrainerClient>(
            j => j.HasOne(tc => tc.Client)
                .WithMany()
                .HasForeignKey(tc => tc.ClientId)
                .OnDelete(DeleteBehavior.Restrict),
            j => j.HasOne(tc => tc.Trainer)
                .WithMany()
                .HasForeignKey(tc => tc.TrainerId)
                .OnDelete(DeleteBehavior.Restrict),
            j =>
            {
                j.HasKey(tc => new { tc.TrainerId, tc.ClientId });
                j.Property(tc => tc.Since).IsRequired();
                j.ToTable("TrainerClients");
            });
    }
}
