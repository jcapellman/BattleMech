using System.Runtime.Serialization;

using BattleMech.PCL.Enums;

namespace BattleMech.PCL.Objects.Game {
    [DataContract]
    public class LevelObject : LevelObjectLite {
        [DataMember]
        public ASSET_TYPES AssetType { get; set; }

        [DataMember]
        public string Filename { get; set; }
    }
}