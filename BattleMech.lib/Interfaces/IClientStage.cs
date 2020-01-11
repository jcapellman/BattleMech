using BattleMech.DataLayer.PCL.Views.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleMech.PCL.Interfaces
{
    public interface IClientStage
    {
        List<ActiveAssetsVIEW> GetAssetInfo(int assetId);
    }
}
