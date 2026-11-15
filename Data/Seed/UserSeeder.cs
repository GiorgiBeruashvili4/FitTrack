using FitTrack.Entities;
using FitTrack.Enums;

namespace FitTrack.Data.Seed;

public static class UserSeeder
{
    public static void Seed(Database db)
    {
        User trainer = new User
        {
            Email = "trainer@gmail.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Trainer123!"),
            FullName = "Trainer Seed",
            Role = Role.Trainer,
            CreatedAt = DateTime.UtcNow
        };

        User member = new User
        {
            Email = "member@gmail.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Member123!"),
            FullName = "Member Seed",
            Role = Role.Member,
            CreatedAt = DateTime.UtcNow
        };

        User stranger = new User
        {
            Email = "stranger@gmail.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Stranger123!"),
            FullName = "Stranger Seed",
            Role = Role.Member,
            CreatedAt = DateTime.UtcNow
        };

        db.Users.AddRange(trainer, member, stranger);
        db.SaveChanges();
    }
}
