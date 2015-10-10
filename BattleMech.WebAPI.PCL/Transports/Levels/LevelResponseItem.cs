using System.Runtime.Serialization;

namespace BattleMech.WebAPI.PCL.Transports.Levels {
    [DataContract]
    public class LevelResponseItem {
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string LevelData { get; set; }
    }
}