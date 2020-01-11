using System.Collections.Generic;
using System.Runtime.Serialization;

using BattleMech.DataLayer.PCL.Views.Assets;

namespace BattleMech.WebAPI.PCL.Transports.Assets {
    [DataContract]
    public class AssetsResponseItem {
        [DataMember]
        public int AssetVerion { get; set; }

        [DataMember]
        public List<ActiveAssetsVIEW> Assets { get; set; }

        [DataMember]
        public bool ClientHasLatest { get; set; }
    }
}