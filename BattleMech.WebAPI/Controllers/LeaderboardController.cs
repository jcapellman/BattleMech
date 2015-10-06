using System.Collections.Generic;

using BattleMech.DataLayer.PCL.Views.Leaderboard;
using BattleMech.WebAPI.Managers;
using BattleMech.WebAPI.PCL.Transports.Common;

namespace BattleMech.WebAPI.Controllers {
    public class LeaderboardController : BaseController {
        public CTI<List<LeaderboardView>> GET() { return new LeaderboardManager().GetLeaderboardList(); }
    }
}