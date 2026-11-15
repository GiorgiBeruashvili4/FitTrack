using FitTrack.Entities;
using FitTrack.Enums;

namespace FitTrack.Data.Seed;

public static class BodyMetricsSeeder
{
    public static void Seed(Database db)
    {
        User member = db.Users.First(u => u.Email == "member@gmail.com");

        db.BodyMetrics.Add(new BodyMetrics
        {
            UserId = member.Id,
            HeightCm = 180,
            WeightKg = 78,
            Goal = Goal.GainMuscle,
            UpdatedAt = DateTime.UtcNow
        });

        db.SaveChanges();
    }
}
