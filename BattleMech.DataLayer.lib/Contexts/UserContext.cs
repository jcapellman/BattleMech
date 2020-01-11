using System;
using System.Linq;
using BattleMech.DataLayer.PCL.Models.GameMetrics;
using BattleMech.DataLayer.PCL.Models.Users;

using Microsoft.EntityFrameworkCore;

namespace BattleMech.DataLayer.Contexts {
    public class UserContext : BaseContext {
        public DbSet<Users> UsersDS { get; set; }

        public DbSet<Tokens> TokensDS { get; set; }

        public DbSet<PlayerCharacters> PlayerCharactersDS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<PlayerCharacters>().Property<DateTimeOffset>("Modified");
            modelBuilder.Entity<PlayerCharacters>().Property<DateTimeOffset>("Created");
            modelBuilder.Entity<PlayerCharacters>().Property<bool>("Active");

            modelBuilder.Entity<Users>().Property<DateTimeOffset>("Modified");
            modelBuilder.Entity<Users>().Property<DateTimeOffset>("Created");
            modelBuilder.Entity<Users>().Property<bool>("Active");
        }

        public override int SaveChanges() {
            var modifiedUsers = ChangeTracker.Entries<Users>().Where(a => a.State == EntityState.Added || a.State == EntityState.Modified);

            foreach (var item in modifiedUsers) {
                if (item.Entity.IsNew) {
                    item.Property("Created").CurrentValue = DateTimeOffset.Now;
                    item.Property<bool>("Active").CurrentValue = true;
                }

                item.Property("Modified").CurrentValue = DateTimeOffset.Now;
            }

            var modifiedCharacters = ChangeTracker.Entries<PlayerCharacters>().Where(a => a.State == EntityState.Added || a.State == EntityState.Modified);

            foreach (var item in modifiedCharacters) {
                if (item.Entity.IsNew) {
                    item.Property("Created").CurrentValue = DateTimeOffset.Now;
                    item.Property<bool>("Active").CurrentValue = true;
                }

                item.Property("Modified").CurrentValue = DateTimeOffset.Now;
            }

            return base.SaveChanges();
        }
    }
}