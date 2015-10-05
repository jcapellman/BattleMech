using BattleMech.WebAPI.Managers;
using BattleMech.WebAPI.PCL.Transports.Auth;

namespace BattleMech.WebAPI.Controllers {
    public class AuthController : BaseController {
        public AuthResponseItem POST(AuthRequestItem requestItem) { return new AuthManager().GenerateToken(requestItem); }
    }
}