using System.Runtime.Serialization;

namespace BattleMech.DataLayer.PCL.Views.Assets {
    [DataContract]
    public class ActiveAssetsVIEW {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Filename { get; set; }

        [DataMember]
        public int AssetTypeID { get; set; }    
    }
}