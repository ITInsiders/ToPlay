using System.Data.Entity;
using TP.ML.Entities;
using TP.ML.IOEntities;

namespace TP.DAL.EF
{
    public partial class Context : DbContext
    {
        public Context() : base("ToPlay") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Gamer> Gamers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<GamingSession> GamingSessions { get; set; }
        public DbSet<GameGamer> GameGamers { get; set; }

        public DbSet<Image> Images { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<GameImage> GameImages { get; set; }
        
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserComment> UserComment { get; set; }
        public DbSet<GameComment> GameComment { get; set; }
        public DbSet<MarkComment> MarkComment { get; set; }

        public DbSet<SystemName> SystemNames { get; set; }

        /* Interesting Opinion */

        public DbSet<Сharacteristic> Сharacteristics { get; set; }
        public DbSet<IOGameGamer> IOGameGamers { get; set; }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public DbSet<Adjective> Adjectives { get; set; }
        public DbSet<TaskAdjective> TaskAdjectives { get; set; }

        public DbSet<СharacteristicAdjective> СharacteristicAdjectives { get; set; }

    }
}
