using BattleMech.DataLayer.PCL.Views.Assets;
using BattleMech.DataLayer.PCL.Views.Levels;

using Microsoft.Data.Entity;

namespace BattleMech.DataLayer.Contexts {
    public class LevelEditorContext : BaseContext {
        public DbSet<ActiveAssetTypes> AssetTypesDS { get; set; } 

        public DbSet<ActiveAssetsVIEW> ActiveAssetsVIEWDS { get; set; } 
    }
}