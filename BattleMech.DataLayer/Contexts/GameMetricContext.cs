using BattleMech.DataLayer.PCL.Models.GameMetrics;
using BattleMech.DataLayer.PCL.Views.GameMetrics;

using Microsoft.Data.Entity;

namespace BattleMech.DataLayer.Contexts {
    public class GameMetricContext : BaseContext {
        public DbSet<PlayerCharacterGames> Games { get; set; }
        
        public DbSet<PlayerGameListingView> GameListingViews { get; set; }
        
        public DbSet<PlayerCharacterView> CharacterViews { get; set; }   
    }
}