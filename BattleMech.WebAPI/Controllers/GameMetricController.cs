using System.Collections.Generic;
using System.Threading.Tasks;

using BattleMech.DataLayer.PCL.Models.GameMetrics;
using BattleMech.DataLayer.PCL.Views.GameMetrics;
using BattleMech.WebAPI.Managers;
using BattleMech.WebAPI.PCL.Transports.Common;

namespace BattleMech.WebAPI.Controllers {
    public class GameMetricController : BaseController {
        public async Task<CTI<bool>> PUT(PlayerCharacterGames gameMetric) {
            return await new GameMetricManager().AddMetric(gameMetric);
        }

        public CTI<List<PlayerGameListingView>> GET(int UserID) {
            return new GameMetricManager().GetMetrics(UserID);
        }
    }
}