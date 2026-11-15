using FitTrack.Entities;
using FitTrack.Enums;

namespace FitTrack.Data.Seed;

public static class DbSeeder
{
    public static void Seed(Database db)
    {
        if (db.Users.Any())
        {
            return;
        }

        UserSeeder.Seed(db);
        MuscleGroupSeeder.Seed(db);
        ExerciseSeeder.Seed(db);
        BodyMetricsSeeder.Seed(db);
        TrainerClientSeeder.Seed(db);
        WorkoutSeeder.Seed(db);
    }
}
