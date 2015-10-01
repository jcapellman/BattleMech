namespace BattleMech.DataLayer.PCL.Views.GameMetrics {
    public class PlayerGameListingView {
        public int ID { get; set; }

        public string LevelName { get; set; }

        public int EnemiesDestroyed { get; set; }

        public int ExperienceGarnered { get; set; }

        public int ShotsFired { get; set; }

        public int TimesDied { get; set; }

        public int UserID { get; set; }

        public int TotalEnemiesFought { get; set; }
    }
}