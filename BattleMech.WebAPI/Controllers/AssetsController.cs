using BattleMech.WebAPI.Managers;
using BattleMech.WebAPI.PCL.Transports.Assets;
using BattleMech.WebAPI.PCL.Transports.Common;

namespace BattleMech.WebAPI.Controllers {
    public class AssetsController : BaseController {
        public CTI<AssetsResponseItem> GET(int clientVersion) {
            return new AssetsManager(AUTH_USER).GetAndCheckAssets(clientVersion);
        }
    }
}