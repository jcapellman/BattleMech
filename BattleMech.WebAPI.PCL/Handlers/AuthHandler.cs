using System.Threading.Tasks;

using BattleMech.WebAPI.PCL.Transports.Auth;
using BattleMech.WebAPI.PCL.Transports.Internal;

namespace BattleMech.WebAPI.PCL.Handlers {
    public class AuthHandler : BaseHandler {
        public AuthHandler(HandlerItem handlerItem) : base(handlerItem, "Auth") { }

        public async Task<AuthResponseItem> GenerateToken(AuthRequestItem requestItem) { return await POST<AuthRequestItem, AuthResponseItem>(string.Empty, requestItem); }
    }
}