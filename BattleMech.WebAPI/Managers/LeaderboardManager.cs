using System;
using System.Collections.Generic;
using System.Linq;

using BattleMech.DataLayer.Contexts;
using BattleMech.DataLayer.PCL.Views.Leaderboard;
using BattleMech.WebAPI.PCL.Transports.Common;

namespace BattleMech.WebAPI.Managers {
    public class LeaderboardManager : BaseManager {
        public CTI<List<LeaderboardView>> GetLeaderboardList() {
            try {
                using (var leaderboardContext = new LeaderboardContext()) {
                    var result = leaderboardContext.LeaderboardListDS.ToList();

                    return new CTI<List<LeaderboardView>>(result);
                }
            } catch (Exception ex) {
                return new CTI<List<LeaderboardView>>(null, ex.ToString());
            }
        } 
    }
}