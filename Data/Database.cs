using FitTrack.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.Data;

public class Database : DbContext
{
    public Database(DbContextOptions<Database> options) : base(options) { }

    public DbSet<BodyMetrics> BodyMetrics { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<ExerciseMuscleGroup> ExerciseMuscleGroups { get; set; }
    public DbSet<MuscleGroup> MuscleGroups { get; set; }
    public DbSet<TrainerClient> TrainerClients { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<WorkoutSet> WorkoutSets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Database).Assembly);
    }
}
