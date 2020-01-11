using BattleMech.DataLayer.PCL.Views.Leaderboard;

using Microsoft.EntityFrameworkCore;

namespace BattleMech.DataLayer.Contexts {
    public class LeaderboardContext : BaseContext {
        public DbSet<LeaderboardView> LeaderboardListDS { get; set; }
    }
}