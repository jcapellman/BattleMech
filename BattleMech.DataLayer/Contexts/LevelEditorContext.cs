using BattleMech.DataLayer.PCL.Models.Levels;
using BattleMech.DataLayer.PCL.Views.Assets;

using Microsoft.Data.Entity;

using System;
using System.Linq;

namespace BattleMech.DataLayer.Contexts {
    public class LevelEditorContext : BaseContext {
        public DbSet<ActiveAssetTypes> ActiveAssetTypesVIEWDS { get; set; } 

        public DbSet<ActiveAssetsVIEW> ActiveAssetsVIEWDS { get; set; }

        public DbSet<Levels> LevelsDS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Levels>().Property<DateTimeOffset>("Modified");
            modelBuilder.Entity<Levels>().Property<DateTimeOffset>("Created");
            modelBuilder.Entity<Levels>().Property<bool>("Active");
        }

        public override int SaveChanges() {
            var modifiedLevels = ChangeTracker.Entries<Levels>().Where(a => a.State == EntityState.Added || a.State == EntityState.Modified);

            foreach (var item in modifiedLevels) {
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