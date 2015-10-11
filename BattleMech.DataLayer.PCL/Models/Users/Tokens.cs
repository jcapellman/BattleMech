using System.Runtime.Serialization;

namespace BattleMech.DataLayer.PCL.Models.Users {
    [DataContract]
    public class Tokens {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string HASH { get; set; }

        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public int CharacterID { get; set; }
    }
}