using System.Runtime.Serialization;

namespace BattleMech.DataLayer.PCL.Views.Leaderboard {
    [DataContract]
    public class LeaderboardView {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string PlayerName { get; set; }

        [DataMember]
        public int LevelNumber { get; set; }

        [DataMember]
        public int Experience { get; set; }

        [DataMember]
        public int Ranking { get; set; }
    }
}