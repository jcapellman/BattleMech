using BattleMech.DataLayer.PCL.Views.Assets;

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BattleMech.WebAPI.PCL.Transports.Levels {
    [DataContract]
    public class LevelEditorResponseItem {
        [DataMember]
        public List<ActiveAssetTypes> AssetTypes { get; set; }
    }
}