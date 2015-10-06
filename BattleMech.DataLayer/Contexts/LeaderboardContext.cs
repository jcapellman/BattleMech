using BattleMech.DataLayer.PCL.Views.Leaderboard;

using Microsoft.Data.Entity;

namespace BattleMech.DataLayer.Contexts {
    public class LeaderboardContext : BaseContext {
        public DbSet<LeaderboardView> LeaderboardListDS { get; set; }
    }
}