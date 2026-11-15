using FitTrack.Entities;

namespace FitTrack.Data.Seed;

public static class TrainerClientSeeder
{
    public static void Seed(Database db)
    {
        User trainer = db.Users.First(u => u.Email == "trainer@gmail.com");
        User member = db.Users.First(u => u.Email == "member@gmail.com");

        db.TrainerClients.Add(new TrainerClient
        {
            TrainerId = trainer.Id,
            ClientId = member.Id,
            Since = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-30))
        });

        db.SaveChanges();
    }
}
