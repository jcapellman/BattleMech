using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BattleMech.WebAPI.PCL.Transports.Levels {
    [DataContract]
    public class LevelEditorRequestItem {
        [DataMember]
        public string LevelName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public List<LevelItemRequestItem> LevelItems { get; set; }
    }
}