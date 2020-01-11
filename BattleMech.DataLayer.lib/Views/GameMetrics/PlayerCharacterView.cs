namespace BattleMech.DataLayer.PCL.Views.GameMetrics {
    public class PlayerCharacterView {
        public int ID { get; set; }

        public int UserID { get; set; }

        public string Name { get; set; }

        public int LevelNumber { get; set; }

        public int Experience { get; set; }

        public int TotalTimesDied { get; set; }

        public int TotalShotsFired { get; set; }

        public int TotalEnemiesDestroyed { get; set; }

        public int TotalEnemiesFought { get; set; }
    }
}