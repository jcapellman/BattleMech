using System.Threading.Tasks;

using BattleMech.DataLayer.PCL.Models.GameMetrics;
using BattleMech.WebAPI.Managers;
using BattleMech.WebAPI.PCL.Transports.Common;

namespace BattleMech.WebAPI.Controllers {
    public class GameMetricController : BaseController {
        public async Task<CTI<bool>> PUT(PlayerCharacterGames gameMetric) {
            return await new GameMetricManager().AddMetric(gameMetric);
        }
    }
}