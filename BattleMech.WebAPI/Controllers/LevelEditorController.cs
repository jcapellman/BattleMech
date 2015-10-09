using BattleMech.WebAPI.Managers;
using BattleMech.WebAPI.PCL.Transports.Common;
using BattleMech.WebAPI.PCL.Transports.Levels;

namespace BattleMech.WebAPI.Controllers {
    public class LevelEditorController : BaseController {
        public CTI<LevelEditorResponseItem> GET() { return new LevelManager(AUTH_USER).GetEditorData(); }
    }
}