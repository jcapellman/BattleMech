using System;
using System.Linq;

using BattleMech.DataLayer.PCL.Models.GameMetrics;
using BattleMech.DataLayer.PCL.Views.GameMetrics;

using Microsoft.Data.Entity;

namespace BattleMech.DataLayer.Contexts {
    public class GameMetricContext : BaseContext {
        public DbSet<PlayerCharacterGames> Games { get; set; }
        
        public DbSet<PlayerGameListingView> GameListingViews { get; set; }
        
        public DbSet<PlayerCharacterView> CharacterViews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<PlayerCharacterGames>().Property<DateTimeOffset>("Modified");
            modelBuilder.Entity<PlayerCharacterGames>().Property<DateTimeOffset>("Created");
            modelBuilder.Entity<PlayerCharacterGames>().Property<bool>("Active");
        }

        public override int SaveChanges() {
            var modifiedGameMetrics = ChangeTracker.Entries<PlayerCharacterGames>().Where(a => a.State == EntityState.Added || a.State == EntityState.Modified);

            foreach (var item in modifiedGameMetrics) {
                if (item.Entity.IsNew) {
                    item.Property<DateTimeOffset>("Created").CurrentValue = DateTimeOffset.Now;
                    item.Property<bool>("Active").CurrentValue = true;
                }

                item.Property<DateTimeOffset>("Modified").CurrentValue = DateTimeOffset.Now;
            }

            return base.SaveChanges();
        }
    }
}