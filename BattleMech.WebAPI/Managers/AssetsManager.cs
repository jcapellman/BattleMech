using System.Linq;

using BattleMech.DataLayer.Contexts;
using BattleMech.WebAPI.PCL.Transports.Assets;
using BattleMech.WebAPI.PCL.Transports.Common;
using BattleMech.WebAPI.Transports.Internal;

namespace BattleMech.WebAPI.Managers {
    public class AssetsManager : BaseManager {
        public AssetsManager(AuthorizedUser user) : base(user) { }

        public CTI<AssetsResponseItem> GetAndCheckAssets(int clientVersion) {
            using (var assetContext = new AssetContext()) {
                var response = new AssetsResponseItem {
                    ClientHasLatest = false,
                    AssetVerion = ++clientVersion,
                    Assets = assetContext.AssetsDS.ToList()
                };
                
                // TODO do database check on assets
                
                return new CTI<AssetsResponseItem>(response);
            }
        }
    }
}