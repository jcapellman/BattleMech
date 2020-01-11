using System.Threading.Tasks;

using BattleMech.WebAPI.PCL.Transports.CharacterProfile;
using BattleMech.WebAPI.PCL.Transports.Common;
using BattleMech.WebAPI.PCL.Transports.Internal;

namespace BattleMech.WebAPI.PCL.Handlers {
    public class CharacterProfileHandler : BaseHandler {
        public CharacterProfileHandler(HandlerItem handlerItem) : base(handlerItem, "CharacterProfile") { }

        public async Task<CTI<CharacterProfileResponseItem>> GET() { return await GET<CTI<CharacterProfileResponseItem>>(string.Empty); }
    }
}