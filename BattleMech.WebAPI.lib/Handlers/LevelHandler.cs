using System.Collections.Generic;
using System.Threading.Tasks;

using BattleMech.DataLayer.PCL.Views.Levels;
using BattleMech.WebAPI.PCL.Transports.Common;
using BattleMech.WebAPI.PCL.Transports.Internal;
using BattleMech.WebAPI.PCL.Transports.Levels;

namespace BattleMech.WebAPI.PCL.Handlers {
    public class LevelHandler : BaseHandler {
        public LevelHandler(HandlerItem handlerItem) : base(handlerItem, "Level") { }

        public async Task<CTI<List<LevelListingView>>> GetLevelList() { return await GET<CTI<List<LevelListingView>>>(string.Empty); }

        public async Task<CTI<LevelResponseItem>> GetLevel(int levelID) {
            return await GET<CTI<LevelResponseItem>>($"levelID={levelID}");
        }
    }
}