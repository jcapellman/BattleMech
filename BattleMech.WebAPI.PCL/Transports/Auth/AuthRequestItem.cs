using System.Runtime.Serialization;

namespace BattleMech.WebAPI.PCL.Transports.Auth {
    [DataContract]
    public class AuthRequestItem {
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}