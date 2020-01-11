using System;
using System.Runtime.Serialization;

namespace BattleMech.DataLayer.PCL.Models.Levels {
    [DataContract]
    public class AssetTypes {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public DateTimeOffset Modified { get; set; }

        [DataMember]
        public DateTimeOffset Created { get; set; }

        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}