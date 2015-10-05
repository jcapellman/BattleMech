using BattleMech.WebAPI.Managers;
using BattleMech.WebAPI.PCL.Transports.CharacterProfile;
using BattleMech.WebAPI.PCL.Transports.Common;

namespace BattleMech.WebAPI.Controllers {
    public class CharacterProfileController : BaseController {
        public CTI<CharacterProfileResponseItem> GET() { return new CharacterProfileManager(AUTH_USER).GetProfile(null); }

        public CTI<CharacterProfileResponseItem> GET(int userID) { return new CharacterProfileManager(AUTH_USER).GetProfile(userID); }
    }
}