using System.Runtime.Serialization;

namespace BattleMech.PCL.Objects.Game {
    [DataContract]
    public class LevelObjectLite {
        [DataMember]
        public int AssetID { get; set; }
        
        [DataMember]
        public int PositionX { get; set; }

        [DataMember]
        public int PositionY { get; set; }
    }
}