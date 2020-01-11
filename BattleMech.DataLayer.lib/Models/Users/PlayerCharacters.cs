using System.Runtime.Serialization;

namespace BattleMech.DataLayer.PCL.Models.Users {
    [DataContract]
    public class PlayerCharacters : BaseModel {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int ClassTypeID { get; set; }

        [DataMember]
        public int LevelNumber { get; set; }

        [DataMember]
        public int Experience { get; set; }

        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public int AssetID { get; set; }
    }
}