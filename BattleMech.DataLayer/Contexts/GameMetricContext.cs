using BattleMech.DataLayer.PCL.Models.GameMetrics;

using Microsoft.Data.Entity;

namespace BattleMech.DataLayer.Contexts {
    public class GameMetricContext : BaseContext {
        public DbSet<PlayerCharacterGames> Games { get; set; }        
    }
}