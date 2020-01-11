using System.Runtime.Serialization;

namespace BattleMech.DataLayer.PCL.Views.Assets {
    [DataContract]
    public class ActiveCharacterAssetsVIEW {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public int AssetID { get; set; }
    }
}