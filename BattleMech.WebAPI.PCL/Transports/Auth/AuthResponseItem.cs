using System.Runtime.Serialization;

namespace BattleMech.WebAPI.PCL.Transports.Auth {
    [DataContract]
    public class AuthResponseItem {
        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public int PlayerAssetID { get; set; }
    }
}