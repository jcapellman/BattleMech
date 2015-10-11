using System.Runtime.Serialization;

namespace BattleMech.DataLayer.PCL.Models.GameMetrics {
    [DataContract]
    public class PlayerCharacterGames : BaseModel {
        [DataMember]
        public int PlayerCharacterID { get; set; }

        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public int LevelID { get; set; }

        [DataMember]
        public int ShotsFired { get; set; }

        [DataMember]
        public int EnemiesDestroyed { get; set; }

        [DataMember]
        public int TotalEnemiesFought { get; set; }

        [DataMember]
        public int ExperienceGarnered { get; set; }

        [DataMember]
        public int TimesDied { get; set; }
    }
}