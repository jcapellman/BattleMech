using System.Threading.Tasks;

using BattleMech.DataLayer.PCL.Models.GameMetrics;
using BattleMech.WebAPI.PCL.Transports.Common;

namespace BattleMech.WebAPI.PCL.Handlers {
    public class GameMetricHandler : BaseHandler {
        public GameMetricHandler(string baseURL) : base(baseURL, "GameMatric") { }

        public async Task<CTI<bool>> AddGameMetric(PlayerCharacterGames gameMetric) { return await PUT<PlayerCharacterGames, bool>(gameMetric); }
    }
}