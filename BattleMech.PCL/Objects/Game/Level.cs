using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BattleMech.PCL.Objects.Game {
    [DataContract]
    public class Level {
        [DataMember]
        public List<LevelObject> Objects { get; set; }
        
        [DataMember]
        public string LevelName { get; set; }
    }
}