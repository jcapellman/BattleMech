using System.Runtime.Serialization;

namespace BattleMech.DataLayer.PCL.Views.Assets {
    [DataContract]
    public class ActiveAssetTypes {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}