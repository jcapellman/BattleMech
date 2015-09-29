namespace BattleMech.DataLayer.PCL.Models.GameMetrics {
    public class PlayerCharacterGames : BaseModel {
        public int PlayerCharacterID { get; set; }

        public int UserID { get; set; }

        public int LevelID { get; set; }

        public int ShotsFired { get; set; }

        public int EnemiesDestroyed { get; set; }

        public int TotalEnemiesFought { get; set; }

        public int ExperienceGarnered { get; set; }

        public int TimesDied { get; set; }
    }
}