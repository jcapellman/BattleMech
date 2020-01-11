using System;
using System.Runtime.Serialization;

namespace BattleMech.DataLayer.PCL.Views.Levels {
    [DataContract]
    public class LevelListingView {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string LevelName { get; set; }

        [DataMember]
        public int NumberOfDownloads { get; set; }

        [DataMember]
        public int Rating { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string AuthorName { get; set; }

        [DataMember]
        public DateTime PublishDate { get; set; }
    }
}