using System.Runtime.Serialization;

using BattleMech.PCL.Enums;
using BattleMech.DataLayer.PCL.Views.Assets;
using System.Collections.Generic;

namespace BattleMech.PCL.Objects.Game {
    [DataContract]
    public class LevelObject : LevelObjectLite {
        [DataMember]
        public List<ActiveAssetsVIEW> AssetInfos { get; set; }
    }
}