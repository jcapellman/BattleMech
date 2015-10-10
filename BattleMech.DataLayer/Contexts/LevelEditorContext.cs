using BattleMech.DataLayer.PCL.Views.Assets;

using Microsoft.Data.Entity;

namespace BattleMech.DataLayer.Contexts {
    public class LevelEditorContext : BaseContext {
        public DbSet<ActiveAssetTypes> ActiveAssetTypesVIEWDS { get; set; } 

        public DbSet<ActiveAssetsVIEW> ActiveAssetsVIEWDS { get; set; } 
    }
}