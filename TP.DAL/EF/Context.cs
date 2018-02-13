using System.Data.Entity;
using TP.DAL.Entities;

namespace TP.DAL.EF
{
    public class Context : DbContext
    {
        private static Context context;
        public static Context I => context ?? (context = new Context());

        public Context() : base("ToPlay") { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserVerification> UserVerifications { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<UserPhoto> UserPhotos { get; set; }
        public DbSet<UserAdministrator> UserAdministrators { get; set; }
    }
}
