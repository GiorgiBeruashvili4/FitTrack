using FitTrack.Entities;

namespace FitTrack.Data.Seed;

public static class WorkoutSeeder
{
    public static void Seed(Database db)
    {
        User member = db.Users.First(u => u.Email == "member@fittrack.com");
        var exercises = db.Exercises.ToDictionary(e => e.Name);
        var today = DateTime.UtcNow.Date;

        Exercise bench = exercises["Bench Press"];
        Exercise pushUp = exercises["Push-Up"];
        Exercise squat = exercises["Squat"];
        Exercise deadlift = exercises["Deadlift"];
        Exercise shoulderPress = exercises["Shoulder Press"];
        Exercise curl = exercises["Dumbbell Curl"];

        Workout chestDay = Create(member.Id, today.AddDays(-1), "Chest Day", 60, "Felt strong today.");
        AddSet(chestDay, bench, 10, 60);
        AddSet(chestDay, bench, 8, 70);
        AddSet(chestDay, bench, 6, 80);
        AddSet(chestDay, pushUp, 15, 0);
        AddSet(chestDay, pushUp, 12, 0);

        Workout legDay = Create(member.Id, today.AddDays(-4), "Leg Day", 75);
        AddSet(legDay, squat, 10, 80);
        AddSet(legDay, squat, 8, 90);
        AddSet(legDay, squat, 6, 100);
        AddSet(legDay, deadlift, 5, 100);
        AddSet(legDay, deadlift, 5, 110);

        Workout upperBody = Create(member.Id, today.AddDays(-8), "Upper Body", 55);
        AddSet(upperBody, bench, 10, 55);
        AddSet(upperBody, bench, 8, 65);
        AddSet(upperBody, bench, 6, 75);
        AddSet(upperBody, shoulderPress, 10, 20);
        AddSet(upperBody, shoulderPress, 10, 22);
        AddSet(upperBody, curl, 12, 12);
        AddSet(upperBody, curl, 10, 14);

        Workout fullBody = Create(member.Id, today.AddDays(-15), "Full Body", 70);
        AddSet(fullBody, squat, 10, 70);
        AddSet(fullBody, squat, 8, 80);
        AddSet(fullBody, bench, 10, 50);
        AddSet(fullBody, bench, 8, 60);

        db.Workouts.AddRange(chestDay, legDay, upperBody, fullBody);
        db.SaveChanges();
    }

    private static Workout Create(
        int userId, DateTime date, string title, int duration, string? notes = null)
    {
        return new Workout
        {
            UserId = userId,
            Date = date,
            Title = title,
            DurationMinutes = duration,
            Notes = notes
        };
    }

    private static void AddSet(Workout workout, Exercise exercise, byte reps, int weightKg)
    {
        byte setNumber = (byte)(workout.WorkoutSets.Count(s => s.Exercise == exercise) + 1);

        workout.WorkoutSets.Add(new WorkoutSet
        {
            Exercise = exercise,
            SetNumber = setNumber,
            Reps = reps,
            WeightKg = weightKg
        });
    }
}
