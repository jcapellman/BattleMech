using BattleMech.DataLayer.PCL.Views.Levels;

using Microsoft.Data.Entity;

namespace BattleMech.DataLayer.Contexts {
    public class LevelContext : BaseContext {
        public DbSet<LevelListingView> LevelListingDS { get; set; }

        public DbSet<ActiveAssetTypes> ActiveAssetTypesDS { get; set; }
    }
}