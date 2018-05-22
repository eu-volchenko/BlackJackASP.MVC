using System.Data.Entity;
using BlackJackDAL.Entities;
using Type = BlackJackDAL.Entities.Type;

namespace BlackJackDAL.EF
{
    public partial class ContextDB : DbContext
    {
        public ContextDB()
            : base("name=DbContext")
        {
        }

        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<GameRound> GameRounds { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<History> Histories { get; set; }
        public virtual DbSet<Round> Rounds { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<UserCard> UserCards { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                .HasMany(e => e.UserCards)
                .WithRequired(e => e.Card)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Game>()
            //    .HasMany(e => e.GameRounds)
            //    .WithRequired(e => e.Game)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Game>()
            //    .HasMany(e => e.Histories)
            //    .WithRequired(e => e.Game)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Round>()
                .HasMany(e => e.GameRounds)
                .WithRequired(e => e.Round)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Type>()
            //    .HasMany(e => e.Users)
            //    .WithRequired(e => e.Type)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<UserCard>()
            //    .HasMany(e => e.Rounds)
            //    .WithRequired(e => e.UserCard)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.Rounds)
            //    .WithRequired(e => e.User)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.UserCards)
            //    .WithRequired(e => e.User)
            //    .WillCascadeOnDelete(false);
        }
    }
}
