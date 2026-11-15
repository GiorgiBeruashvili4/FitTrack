using FitTrack.Entities;

namespace FitTrack.Data.Seed;

public static class MuscleGroupSeeder
{
    public static void Seed(Database db)
    {
        string[] names =
        {
            "Chest",
            "Back",
            "Legs",
            "Shoulders",
            "Biceps",
            "Triceps",
            "Core"
        };

        foreach(var name in names)
        {
            db.MuscleGroups.Add(new MuscleGroup { Name = name });
        }

        db.SaveChanges();
    }
}
