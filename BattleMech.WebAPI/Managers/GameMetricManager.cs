using System;
using System.Threading.Tasks;

using BattleMech.DataLayer.Contexts;
using BattleMech.DataLayer.PCL.Models.GameMetrics;
using BattleMech.WebAPI.PCL.Transports.Common;

namespace BattleMech.WebAPI.Managers {
    public class GameMetricManager : BaseManager {
        public async Task<CTI<bool>> AddMetric(PlayerCharacterGames gameMetric) {
            try {
                using (var db = new GameMetricContext()) {
                    db.Games.Add(gameMetric);

                    var result = await db.SaveChangesAsync();

                    return new CTI<bool>(result > 0);
                }
            } catch (Exception ex) {
                return new CTI<bool>(false, ex.ToString());
            }
        }
    }
}