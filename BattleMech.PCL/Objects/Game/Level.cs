using System.Collections.Generic;

namespace BattleMech.PCL.Objects.Game {
    public class Level {
        public List<LevelObject> Objects { get; set; }
        
        public string LevelName { get; set; } 
    }
}