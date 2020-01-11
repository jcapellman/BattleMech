using System.Runtime.Serialization;

namespace BattleMech.DataLayer.PCL.Models.Levels {
    [DataContract]
    public class Levels : BaseModel {
        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Data { get; set; }

        [DataMember]
        public int NumDownloads { get; set; }

        [DataMember]
        public int UserAuthorID { get; set; }

        [DataMember]
        public int Rating { get; set; }
    }
}