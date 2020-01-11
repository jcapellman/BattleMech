using BattleMech.DataLayer.PCL.Views.Assets;

using Microsoft.EntityFrameworkCore;

namespace BattleMech.DataLayer.Contexts {
    public class AssetContext : BaseContext {
        public DbSet<ActiveAssetsVIEW> AssetsDS { get; set; }

        public DbSet<ActiveCharacterAssetsVIEW> CharacterAssetsVIEWDS { get; set; } 
    }
}