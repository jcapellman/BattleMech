using System.Runtime.Serialization;

namespace BattleMech.DataLayer.PCL.Views.Levels {
    [DataContract]
    public class LevelVIEW {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Data { get; set; }
    }
}