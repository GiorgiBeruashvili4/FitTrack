using FitTrack.Entities;
using FitTrack.Enums;

namespace FitTrack.Data.Seed;

public static class ExerciseSeeder
{
    public static void Seed(Database db)
    {
        var groups = db.MuscleGroups.ToDictionary(m => m.Name);

        Exercise bench = Create("Bench Press", "Barbell press on a flat bench.", Equipment.Barbell);
        AddMuscleGroup(bench, groups["Chest"]);
        AddMuscleGroup(bench, groups["Triceps"]);

        Exercise squat = Create("Squat", "Barbell back squat.", Equipment.Barbell);
        AddMuscleGroup(squat, groups["Legs"]);
        AddMuscleGroup(squat, groups["Core"]);

        Exercise deadlift = Create("Deadlift", "Lift a barbell from the floor to hip level.", Equipment.Barbell);
        AddMuscleGroup(deadlift, groups["Back"]);
        AddMuscleGroup(deadlift, groups["Legs"]);

        Exercise curl = Create("Dumbbell Curl", "Standing biceps curl with dumbbells.", Equipment.Dumbbell);
        AddMuscleGroup(curl, groups["Biceps"]);

        Exercise shoulderPress = Create("Shoulder Press", "Overhead press with dumbbells.", Equipment.Dumbbell);
        AddMuscleGroup(shoulderPress, groups["Shoulders"]);
        AddMuscleGroup(shoulderPress, groups["Triceps"]);

        Exercise pushUp = Create("Push-Up", "Bodyweight push-up.", Equipment.Bodyweight);
        AddMuscleGroup(pushUp, groups["Chest"]);
        AddMuscleGroup(pushUp, groups["Core"]);

        Exercise plank = Create("Plank", "Static core hold.", Equipment.None);
        AddMuscleGroup(plank, groups["Core"]);

        db.Exercises.AddRange(bench, squat, deadlift, curl, shoulderPress, pushUp, plank);
        db.SaveChanges();
    }

    private static Exercise Create(string name, string description, Equipment equipment)
    {
        return new Exercise
        {
            Name = name,
            Description = description,
            Equipment = equipment
        };
    }

    private static void AddMuscleGroup(Exercise exercise, MuscleGroup muscleGroup)
    {
        exercise.ExerciseMuscleGroups.Add(new ExerciseMuscleGroup { MuscleGroup = muscleGroup });
    }
}
