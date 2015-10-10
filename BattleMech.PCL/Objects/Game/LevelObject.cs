using System.Runtime.Serialization;

using BattleMech.PCL.Enums;

namespace BattleMech.PCL.Objects.Game {
    [DataContract]
    public class LevelObject {
        [DataMember]
        public int AssetID { set; get; }

        [DataMember]
        public string TextureName { get; set; }

        [DataMember]
        public int PositionX { get; set; }

        [DataMember]
        public int PositionY { get; set; }

        [DataMember]
        public ASSET_TYPES AssetType { get; set; }
    }
}