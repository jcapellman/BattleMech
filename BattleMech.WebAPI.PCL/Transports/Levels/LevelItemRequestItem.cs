using System.Runtime.Serialization;

namespace BattleMech.WebAPI.PCL.Transports.Levels {
    [DataContract]
    public class LevelItemRequestItem {
        [DataMember]
        public int AssetID { get; set; }

        [DataMember]
        public int ZIndex { get; set; }

        [DataMember]
        public int XIndex { get; set; }
    }
}