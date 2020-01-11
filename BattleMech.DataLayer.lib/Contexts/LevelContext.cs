using BattleMech.DataLayer.PCL.Views.Levels;

using Microsoft.EntityFrameworkCore;

namespace BattleMech.DataLayer.Contexts {
    public class LevelContext : BaseContext {
        public DbSet<LevelListingView> LevelListingDS { get; set; }     
        
        public DbSet<LevelVIEW> LevelVIEWDS { get; set; }    
    }
}