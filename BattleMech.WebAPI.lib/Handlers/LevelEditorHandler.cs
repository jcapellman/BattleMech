using System.Threading.Tasks;

using BattleMech.WebAPI.PCL.Transports.Common;
using BattleMech.WebAPI.PCL.Transports.Internal;
using BattleMech.WebAPI.PCL.Transports.Levels;
using BattleMech.DataLayer.PCL.Models.Levels;

namespace BattleMech.WebAPI.PCL.Handlers {
    public class LevelEditorHandler : BaseHandler {
        public LevelEditorHandler(HandlerItem handlerItem) : base(handlerItem, "LevelEditor") { }

        public async Task<CTI<bool>> AddUpdateLevel(Levels level) {
            return await PUT<Levels, CTI<bool>>(level);
        }

        public async Task<CTI<LevelEditorResponseItem>> GetLevelEditorData() {
            return await GET<CTI<LevelEditorResponseItem>>(string.Empty);
        }
    }
}