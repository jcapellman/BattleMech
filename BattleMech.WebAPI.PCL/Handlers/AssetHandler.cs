using System.Threading.Tasks;

using BattleMech.WebAPI.PCL.Transports.Assets;
using BattleMech.WebAPI.PCL.Transports.Common;
using BattleMech.WebAPI.PCL.Transports.Internal;

namespace BattleMech.WebAPI.PCL.Handlers {
    public class AssetHandler : BaseHandler {
        public AssetHandler(HandlerItem handlerItem) : base(handlerItem, "Assets") { }

        public async Task<CTI<AssetsResponseItem>> GetAssets(int clientVersion) {
            return await GET<CTI<AssetsResponseItem>>($"clientVersion={clientVersion}");
        }
    }
}