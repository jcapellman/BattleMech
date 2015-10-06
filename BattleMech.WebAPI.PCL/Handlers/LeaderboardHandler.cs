using System.Collections.Generic;
using System.Threading.Tasks;

using BattleMech.DataLayer.PCL.Views.Leaderboard;
using BattleMech.WebAPI.PCL.Transports.Common;
using BattleMech.WebAPI.PCL.Transports.Internal;

namespace BattleMech.WebAPI.PCL.Handlers {
    public class LeaderboardHandler : BaseHandler {
        public LeaderboardHandler(HandlerItem handlerItem) : base(handlerItem, "Leaderboard") { }

        public async Task<CTI<List<LeaderboardView>>> GetLeaderboardList() { return await GET<CTI<List<LeaderboardView>>>(string.Empty); }
    }
}